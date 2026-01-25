using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkDeleteMigrator.Models
{
    public class BulkDeletionJob
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FetchXml { get; set; }
        public string TableLogicalName { get; set; }
        public string TableDisplayName { get; set; }
        public string Frequency { get; set; }
        public string Interval { get; set; }
        public DateTime StartedOn { get; set; }
        public string Status { get; set; }
        public string StatusReason { get; set; }
        public string RecurrencePattern { get; set; }
        public string TableNameCombined
        {
            get
            {
                if (string.IsNullOrWhiteSpace(TableDisplayName))
                {
                    return TableLogicalName;
                }
                return $"{TableDisplayName} ({TableLogicalName})";
            }
        }
        public string StartedOnLocal
        {
            get
            {
                if (StartedOn != null)
                {
                    return StartedOn.ToLocalTime().ToString("g");
                }
                return "N/A";
            }
        }
    }
}
