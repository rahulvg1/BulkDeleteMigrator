using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace BulkDeleteMigrator.Helpers
{
    public class BulkDeletionHelper
    {
        public static string ExtractFetchXml(string bulkDeleteData)
        {
            XDocument doc = XDocument.Parse(bulkDeleteData);

            // Find the node with the name string
            var fetchXmlNode = doc.Descendants("string").FirstOrDefault();
            if (fetchXmlNode != null && fetchXmlNode.Value != null)
            {
                return fetchXmlNode.Value;
            }
            return null;

        }

        public static string ExtractTableName(string fetchXML)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(fetchXML);
            var entityNode = xmlDoc.SelectSingleNode("fetch/entity");
            if (entityNode != null && entityNode.Attributes != null && entityNode.Attributes["name"] != null)
            {
                return entityNode.Attributes["name"].Value;
            }
            return null;

        }

        public static string GetTableDisplayName(string tableLogicalName, IOrganizationService service)
        {
            var request = new RetrieveEntityRequest
            {
                EntityFilters = EntityFilters.Entity,
                LogicalName = tableLogicalName
            };

            var response = (RetrieveEntityResponse)service.Execute(request);
            var tableDisplayName = response.EntityMetadata.DisplayName.UserLocalizedLabel?.Label;
            return tableDisplayName;
        }

        public static (string frequency, string interval) ExtractRecurrenceDetails(string recurrencePattern)
        {
            string frequency = "";
            string interval = "";

            if (!String.IsNullOrWhiteSpace(recurrencePattern))
            {
                string[] recurrenceParts = recurrencePattern.Split(';');
                foreach (var part in recurrenceParts)
                {
                    var prop = part.Split('=');
                    if (prop[0] == "FREQ")
                    {
                        frequency = prop[1];
                    }
                    else if (prop[0] == "INTERVAL")
                    {
                        interval = prop[1];
                    }
                }
            }
            return (frequency, interval);
        }
    }
}
