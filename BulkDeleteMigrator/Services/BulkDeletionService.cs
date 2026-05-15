using BulkDeleteMigrator.Helpers;
using BulkDeleteMigrator.Models;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using XrmToolBox.Extensibility;

namespace BulkDeleteMigrator.Services
{
    public class BulkDeletionService
    {
        private readonly IOrganizationService _service;
        // Bulk Delete Operation Type to be used in Query
        private const int BulkDeleteOperationType = 13;
        public BulkDeletionService(IOrganizationService service)
        {
            _service = service;
        }
        public EntityCollection FetchJobs(int jobType)
        {
 
            // Recurring Jobs
            if (jobType == 0)
            {
                var recurringJobs = FetchRecurringJobs();
                return recurringJobs;
                
            }
            else if (jobType == 1) // Non recurring jobs
            {
                var minimalRecurringJobs = FetchRecurringJobs(true);
                // Build a Hashset of all correlation Ids of recurring jobs
                var recurringCorrelationIds = BuildCorrelationIdHashSet(minimalRecurringJobs);

                // Fetch all non recurring jobs which will include instances of recurring jobs and filter them out
                var nonRecurringJobs = FetchNonRecurringJobs();
                var trueNonRecurringJobs = FilterNonRecurringJobs(nonRecurringJobs, recurringCorrelationIds);

                var trueNonRecurringJobsList = new EntityCollection
                {
                    EntityName = "asyncoperation"
                };
                trueNonRecurringJobsList.Entities.AddRange(trueNonRecurringJobs);
                return trueNonRecurringJobsList;
            }
            else // All jobs
            {
                var recurringJobs = FetchRecurringJobs();
                // Build a Hashset of all correlation Ids of recurring jobs
                var recurringCorrelationIds = BuildCorrelationIdHashSet(recurringJobs);
                
                // Fetch all non recurring jobs which will include instances of recurring jobs and filter them out
                var nonRecurringJobs = FetchNonRecurringJobs();
                var trueNonRecurringJobs = FilterNonRecurringJobs(nonRecurringJobs, recurringCorrelationIds);

                var finalJobsCollection = new EntityCollection
                {
                    EntityName = "asyncoperation"
                };
                finalJobsCollection.Entities.AddRange(recurringJobs.Entities);
                finalJobsCollection.Entities.AddRange(trueNonRecurringJobs);

                return finalJobsCollection;
            }
        }

        private EntityCollection FetchRecurringJobs(bool minimalFields=false)
        {
            var query = new QueryExpression("asyncoperation");
            if (minimalFields)
            {
                query.ColumnSet.AddColumns("correlationid");
            }
            else
            {
                query.ColumnSet.AddColumns("name", "recurrencepattern", "recurrencestarttime", "data", "statecode", "statuscode", "correlationid");
            }
            query.Criteria.AddCondition("operationtype", ConditionOperator.Equal, BulkDeleteOperationType);
            query.Criteria.AddCondition("recurrencepattern", ConditionOperator.NotNull);
            query.AddOrder("name", OrderType.Ascending);
            var results = _service.RetrieveMultiple(query);
            return results;
        }

        private EntityCollection FetchNonRecurringJobs()
        {
            var nonRecurringQuery = new QueryExpression("asyncoperation");
            nonRecurringQuery.ColumnSet.AddColumns("name", "recurrencepattern", "recurrencestarttime", "data", "statecode", "statuscode", "correlationid");
            nonRecurringQuery.Criteria.AddCondition("operationtype", ConditionOperator.Equal, BulkDeleteOperationType);
            nonRecurringQuery.Criteria.AddCondition("recurrencepattern", ConditionOperator.Null);
            nonRecurringQuery.AddOrder("name", OrderType.Ascending);
            var nonRecurringQueryResults = _service.RetrieveMultiple(nonRecurringQuery);
            return nonRecurringQueryResults;
        }

        private HashSet<Guid> BuildCorrelationIdHashSet(EntityCollection recurringJobs)
        {
            var recurringCorrelationIds = new HashSet<Guid>();
            foreach (var record in recurringJobs.Entities)
            {
                var correlationId = record.GetAttributeValue<Guid>("correlationid");
                if (correlationId != Guid.Empty)
                {
                    recurringCorrelationIds.Add(correlationId);
                }
            }
            return recurringCorrelationIds;
        }

        private List<Entity> FilterNonRecurringJobs(EntityCollection nonRecurringJobs, HashSet<Guid> recurringCorrelationIds)
        {
            var trueNonRecurringJobs = nonRecurringJobs.Entities.Where((record) => {

                var nonRecurringCorrelationId = record.GetAttributeValue<Guid>("correlationid");
                // Exclude record if the correlation id of the non recurring job matches the recurring
                if (nonRecurringCorrelationId != Guid.Empty && recurringCorrelationIds.Contains(nonRecurringCorrelationId))
                {
                    return false;
                }
                return true;
            }).ToList();

            return trueNonRecurringJobs;

        }

        public List<BulkDeletionJob> ProcessJobs(EntityCollection bulkDeletionJobsResult)
        {
            var bulkDeletionJobList = new List<BulkDeletionJob>();
            if (bulkDeletionJobsResult != null)
            {
                foreach (var record in bulkDeletionJobsResult.Entities)
                {
                    var bulkDeleteData = (string)record["data"];
                    var fetchXml = BulkDeletionHelper.ExtractFetchXml(bulkDeleteData);

                    var tableLogicalName = BulkDeletionHelper.ExtractTableName(fetchXml);
                    var tableDisplayName = BulkDeletionHelper.GetTableDisplayName(tableLogicalName, _service);

                    var recurrencePattern = record.GetAttributeValue<string>("recurrencepattern")??string.Empty;
                    var (frequency, interval) = BulkDeletionHelper.ExtractRecurrenceDetails(recurrencePattern);
                    
                    bulkDeletionJobList.Add(new BulkDeletionJob
                    {
                        Id = record.Id,
                        Name = record.GetAttributeValue<string>("name") ?? "",
                        FetchXml = fetchXml,
                        TableLogicalName = tableLogicalName,
                        TableDisplayName = tableDisplayName,
                        Type = String.IsNullOrWhiteSpace(recurrencePattern) ? "Non-Recurring" : "Recurring",
                        Frequency = frequency,
                        Interval = interval,
                        RecurrencePattern = recurrencePattern,
                        StartedOn = record.GetAttributeValue<DateTime?>("recurrencestarttime"),
                        Status = record.FormattedValues["statecode"],
                        StatusReason = record.FormattedValues["statuscode"]
                    });
                }
            }
            return bulkDeletionJobList;
        }

        public void MigrateJob(BulkDeletionJob job)
        {
            // Convert FetchXML to QueryExpression as BulkDelete Request only accepts QueryExpressions
            FetchXmlToQueryExpressionRequest fetchXmlToQueryExpressionRequest = new FetchXmlToQueryExpressionRequest()
            {
                FetchXml = job.FetchXml
            };
            FetchXmlToQueryExpressionResponse fetchXmlToQueryExpressionResponse =
            _service.Execute(fetchXmlToQueryExpressionRequest) as FetchXmlToQueryExpressionResponse;

            QueryExpression queryExpression = fetchXmlToQueryExpressionResponse.Query;

            BulkDeleteRequest bulkDeleteRequest = new BulkDeleteRequest()
            {
                QuerySet = new[] { queryExpression },
                StartDateTime = DateTime.UtcNow,
                RecurrencePattern = job.RecurrencePattern,
                SendEmailNotification = false,
                JobName = job.Name,
                ToRecipients = Array.Empty<Guid>(),
                CCRecipients = Array.Empty<Guid>()
            };
            _service.Execute(bulkDeleteRequest);
        }
    }
}
