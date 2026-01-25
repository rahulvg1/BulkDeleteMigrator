using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using BulkDeleteMigrator.Helpers;
using BulkDeleteMigrator.Services;


namespace BulkDeleteMigrator
{
    public partial class BulkDeleteMigrator : MultipleConnectionsPluginControlBase
    {
        private Settings mySettings;

        public BulkDeleteMigrator()
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
            }

            if (actionName == "AdditionalOrganization")
            {
                SetEnvironmentName(detail.ConnectionName, detail.OrganizationFriendlyName, "Target");
            }
            else
            {   // Load bulk delete jobs when tool is opened with user already connect to an environment
                ExecuteMethod(LoadBulkDeletionJobs);

                SetEnvironmentName(detail.ConnectionName, detail.OrganizationFriendlyName, "Source");
            }
        }
        protected override void ConnectionDetailsUpdated(NotifyCollectionChangedEventArgs e)
        {
        }

        private void LoadJobsButton_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadBulkDeletionJobs);
        }
        private void TargetEnvButton_Click(object sender, EventArgs e)
        {
            AddAdditionalOrganization();
        }
        private void SelectAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool selectedValue = selectAllCheckBox.Checked;
            foreach (DataGridViewRow row in jobsDataGridView.Rows)
            {
                row.Cells[0].Value = selectedValue;
            }
        }

        private void LoadBulkDeletionJobs()
        {
            if (Service != null)
            {
                var bulkDeletionService = new BulkDeletionService(Service);

                WorkAsync(new WorkAsyncInfo()
                {
                    Message = "Retrieving Bulk Deletion Jobs...",
                    Work = (worker, args) =>
                    {
                        args.Result = bulkDeletionService.FetchJobs();
                    },
                    PostWorkCallBack = (args) =>
                    {
                        if (args.Error != null)
                        {
                            MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        var bulkDeletionJobsList = bulkDeletionService.ProcessJobs(args.Result as EntityCollection);

                        // Clear data grid before adding rows
                        jobsDataGridView.Rows.Clear();

                        foreach (var job in bulkDeletionJobsList)
                        {
                            int rowIndex = jobsDataGridView.Rows.Add(false, job.Name, job.TableNameCombined, job.Frequency,
                            job.Interval, job.StartedOnLocal, job.Status, job.StatusReason);

                            jobsDataGridView.Rows[rowIndex].Tag = job;
                        }
                    }

                });
            }
        }
        private void MigrateJobsButton_Click(object sender, EventArgs e)
        {


        }

        private void SetEnvironmentName(String envNameXrm, string envNameFriendly, String envType)
        {
            if (envType == "Target")
            {
                targetEnvLabel.Text = $"{envNameFriendly} ({envNameXrm})";
                targetEnvLabel.ForeColor = Color.Green;
            }
            else
            {
                sourceEnvLabel.Text = $"{envNameFriendly} ({envNameXrm})";
                sourceEnvLabel.ForeColor = Color.Green;
            }
        }
    }
}