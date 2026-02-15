using BulkDeleteMigrator.Models;
using BulkDeleteMigrator.Services;
using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;


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
                // Clear connection and add it again to ensure only one additional connection is present
                AdditionalConnectionDetails.Clear();
                AdditionalConnectionDetails.Add(detail);
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
                        EntityCollection rawResults = bulkDeletionService.FetchJobs();
                        if (rawResults?.Entities?.Count > 0)
                        {
                            args.Result = bulkDeletionService.ProcessJobs(rawResults);
                        }
                        else
                        {
                            args.Result = new List<BulkDeletionJob>();
                        }
                    },
                    PostWorkCallBack = (args) =>
                    {
                        if (args.Error != null)
                        {
                            MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        var bulkDeletionJobsList = args.Result as List<BulkDeletionJob>;

                        // Clear data grid before adding rows
                        jobsDataGridView.Rows.Clear();

                        if (bulkDeletionJobsList.Count == 0)
                        {
                            MessageBox.Show("No Bulk Deletion Jobs found in this environment.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        // Enable Migrate and Select Target buttons and Select/Unselect All checkbox 
                        EnableInputElements();
                        // Clear select all checkbox when jobs are reloaded
                        selectAllCheckBox.Checked = false;

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
            // DataGrid waits until we click away to commit changes. To immediately commmit them so we get the accurate selection information, call EndEdit
            jobsDataGridView.EndEdit();
            
            if (AdditionalConnectionDetails.Count == 0)
            {
                MessageBox.Show("Please select a Target Environment to proceed", "Warning", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool isJobsSelected = IsJobsSelected();
            if (!isJobsSelected)
            {
                MessageBox.Show("Please select at least one Bulk Deletion Job to proceed","Warning", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var jobsToMigrate = new List<BulkDeletionJob>();
            foreach(DataGridViewRow row in jobsDataGridView.Rows)
            {
                // If row is selected add the BulkDeletionJob object to the list
                if (Convert.ToBoolean(row.Cells[0].Value))
                {
                    jobsToMigrate.Add(row.Tag as BulkDeletionJob);
                }
            }
            MigrateBulkDeletionJobs(jobsToMigrate);
        }

        private void MigrateBulkDeletionJobs(List<BulkDeletionJob> jobsToMigrate)
        {
            var targetService = AdditionalConnectionDetails.First().GetCrmServiceClient();
            var bulkDeletionService = new BulkDeletionService(targetService);

            WorkAsync(new WorkAsyncInfo()
            {
                Message = "Migrating Jobs...",
                Work = (worker, args) =>
                {
                    int successCount = 0;
                    int errorCount = 0;

                    WriteLog($"Starting Migration of {jobsToMigrate.Count} Job(s)");

                    foreach (BulkDeletionJob job in jobsToMigrate)
                    {
                        try
                        {
                            bulkDeletionService.MigrateJob(job);
                            WriteLog($"Successfully migrated: {job.Name}");
                            successCount++;
                        }
                        catch (Exception ex)
                        {
                            WriteLog($"Error migrating {job.Name}: {ex.Message}");
                            errorCount++;
                        }
                    }
                    args.Result = new { SuccessCount = successCount, ErrorCount = errorCount };
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    var result = (dynamic)args.Result;

                    WriteLog($"Migration Completed. Success: {result.SuccessCount}, Failures: {result.ErrorCount}");
                    WriteLog("=========================================================");

                    if (result.ErrorCount > 0)
                    {
                        MessageBox.Show($"Migration completed with errors.\nSuccess: {result.SuccessCount}\nFailures: {result.ErrorCount}\nPlease check the logs for more details.",
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show($"Migration completed successfully", "Success");
                    }
                    ClearJobSelections();
                }
            });

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

        private bool IsJobsSelected()
        {
            foreach (DataGridViewRow row in jobsDataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value))
                {
                    return true;
                }
            }
            return false;
        }

        private void ClearJobSelections()
        {
            // Clear selections in Datagrid
            foreach (DataGridViewRow row in jobsDataGridView.Rows)
            {
                row.Cells[0].Value = false;
            }

            // Clear Select All Checkbox
            if (selectAllCheckBox.Checked)
            {
                selectAllCheckBox.Checked = false;
            }
        }

        private void WriteLog(string message)
        { 
            logTextBox.AppendText(message + Environment.NewLine);

        }

        private void EnableInputElements()
        {
            selectAllCheckBox.Enabled = true;
            targetEnvButton.Enabled = true;
            migrateJobsButton.Enabled = true;

        }
    }
}