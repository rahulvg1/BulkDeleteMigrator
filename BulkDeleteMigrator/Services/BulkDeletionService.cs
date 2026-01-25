using BulkDeleteMigrator.Helpers;
using BulkDeleteMigrator.Models;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;

namespace BulkDeleteMigrator.Services
{
    public class BulkDeletionService
    {
        private readonly IOrganizationService _service;
        public BulkDeletionService(IOrganizationService service)
        {
            _service = service;
        }
        public EntityCollection FetchJobs()
        {
            // Operation Type - Bulk Delete
            const int queryOperationType = 13;

            var query = new QueryExpression("asyncoperation");
            query.ColumnSet.AddColumns("name", "statecode", "statuscode", "recurrencepattern", "recurrencestarttime", "data");

            query.Criteria.AddCondition("operationtype", ConditionOperator.Equal, queryOperationType);
            query.Criteria.AddCondition("recurrencepattern", ConditionOperator.NotNull);

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

                    var recurrencePattern = (string)record["recurrencepattern"];
                    var (frequency, interval) = BulkDeletionHelper.ExtractRecurrenceDetails(recurrencePattern);

                    bulkDeletionJobList.Add(new BulkDeletionJob
                    {
                        Id = record.Id,
                        Name = record.GetAttributeValue<string>("name") ?? "",
                        FetchXml = fetchXml,
                        TableLogicalName = tableLogicalName,
                        TableDisplayName = tableDisplayName,
                        Frequency = frequency,
                        Interval = interval,
                        RecurrencePattern = recurrencePattern,
                        StartedOn = record.GetAttributeValue<DateTime?>("recurrencestarttime").Value,
                        Status = record.FormattedValues["statecode"],
                        StatusReason = record.FormattedValues["statuscode"]
                    });
                }
            }
            return bulkDeletionJobList;
        }
    }
}
