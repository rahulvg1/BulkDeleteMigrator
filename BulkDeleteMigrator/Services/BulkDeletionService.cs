using BulkDeleteMigrator.Helpers;
using BulkDeleteMigrator.Models;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
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
        public BulkDeletionService(IOrganizationService service)
        {
            _service = service;
        }
        public EntityCollection FetchJobs(int jobType)
        {
            // Operation Type - Bulk Delete
            const int queryOperationType = 13;

            var query = new QueryExpression("asyncoperation");
            query.ColumnSet.AddColumns("name", "recurrencepattern", "recurrencestarttime", "data", "statecode", "statuscode");

            query.Criteria.AddCondition("operationtype", ConditionOperator.Equal, queryOperationType);

            if (jobType == 0)
            {
                query.Criteria.AddCondition("recurrencepattern", ConditionOperator.NotNull);
            }
            else if (jobType == 1)
            {
                query.Criteria.AddCondition("recurrencepattern", ConditionOperator.Null);
            }

            query.AddOrder("name", OrderType.Ascending);

            var results = _service.RetrieveMultiple(query);
            return results;
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

                    var recurrencePattern = record.GetAttributeValue<string>("recurrencepattern");
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
