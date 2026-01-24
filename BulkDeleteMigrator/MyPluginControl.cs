using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using XrmToolBox.Extensibility;

namespace BulkDeleteMigrator
{
    public partial class MyPluginControl : PluginControlBase
    {
        private Settings mySettings;

        public MyPluginControl()
        {
            InitializeComponent();
        }

        private void MyPluginControl_Load(object sender, EventArgs e)
        {
            // Loads or creates the settings for the plugin
            if (!SettingsManager.Instance.TryLoad(GetType(), out mySettings))
            {
                mySettings = new Settings();

                LogWarning("Settings not found => a new settings file has been created!");
            }
            else
            {
                LogInfo("Settings found and loaded");
            }
            ExecuteMethod(LoadBulkDeletionJobs);

        }

        /// <summary>
        /// This event occurs when the plugin is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
        {
            // Before leaving, save the settings
            SettingsManager.Instance.Save(GetType(), mySettings);
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            if (mySettings != null && detail != null)
            {
                mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
                LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
                ExecuteMethod(LoadBulkDeletionJobs);
            }
        }

        private void LoadJobsButton_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadBulkDeletionJobs);
        }

        private void LoadBulkDeletionJobs()
        {
            WorkAsync(new WorkAsyncInfo()
            {
                Message = "Retrieving Bulk Deletion Jobs...",
                Work = (worker, args) =>
                {
                    // Operation Type - Bulk Delete
                    const int queryOperationType = 13;

                    var query = new QueryExpression("asyncoperation");
                    query.ColumnSet.AddColumns("name", "statuscode", "recurrencepattern", "recurrencestarttime", "data");

                    query.Criteria.AddCondition("operationtype", ConditionOperator.Equal, queryOperationType);
                    query.Criteria.AddCondition("recurrencepattern", ConditionOperator.NotNull);

                    query.AddOrder("createdon", OrderType.Descending);

                    args.Result = Service.RetrieveMultiple(query);
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    var result = args.Result as EntityCollection;
                    jobsDataGridView.Rows.Clear();
                    if (result != null)
                    {
                        foreach (var record in result.Entities)
                        {
                            string name = record.GetAttributeValue<string>("name") ?? "";
                            string status = record.FormattedValues["statuscode"];

                            var recurrence = (string)record["recurrencepattern"];
                            var recurrenceDetails = ExtractRecurrenceDetails(recurrence);

                            String recurrenceStart = record.GetAttributeValue<DateTime?>("recurrencestarttime").Value.ToLocalTime().ToString("g") ?? "";

                            var bulkDeleteData = (string)record["data"];
                            var fetchXml = ExtractFetchXml(bulkDeleteData);

                            var tableLogicalName = ExtractTableName(fetchXml);
                            var tableDisplayName = GetTableDisplayName(tableLogicalName, Service);
                            var tableNameGrid = !String.IsNullOrWhiteSpace(tableDisplayName) ?
                            $"{tableDisplayName} ({tableLogicalName})" : tableLogicalName;

                            jobsDataGridView.Rows.Add(false, name, tableNameGrid, recurrenceDetails.frequency, 
                                recurrenceDetails.interval, recurrenceStart, status);

                        }

                    }
                }

            });
        }

        private string ExtractFetchXml(string bulkDeleteData)
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

        private string ExtractTableName(string fetchXML)
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

        public string GetTableDisplayName(string tableLogicalName, IOrganizationService service)
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

        public (string frequency, string interval) ExtractRecurrenceDetails(string recurrencePattern)
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