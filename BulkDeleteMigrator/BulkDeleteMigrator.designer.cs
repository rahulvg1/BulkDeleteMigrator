namespace BulkDeleteMigrator
{
    partial class BulkDeleteMigrator
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.loadJobsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.migrateJobsButton = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.targetEnvLabel = new System.Windows.Forms.Label();
            this.targetEnvButton = new System.Windows.Forms.Button();
            this.sourceEnvLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.jobsDataGridView = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.selectAllCheckBox = new System.Windows.Forms.CheckBox();
            this.JobSelection = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.JobName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Table = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Frequency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Interval = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecurrenceDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusReason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStripMenu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.jobsDataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadJobsButton,
            this.toolStripSeparator1,
            this.migrateJobsButton});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(1215, 31);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // loadJobsButton
            // 
            this.loadJobsButton.Image = global::BulkDeleteMigrator.Properties.Resources.load_icon_24;
            this.loadJobsButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.loadJobsButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.loadJobsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadJobsButton.Name = "loadJobsButton";
            this.loadJobsButton.Size = new System.Drawing.Size(160, 28);
            this.loadJobsButton.Text = "Load Bulk Deletion Jobs";
            this.loadJobsButton.Click += new System.EventHandler(this.LoadJobsButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // migrateJobsButton
            // 
            this.migrateJobsButton.Image = global::BulkDeleteMigrator.Properties.Resources.play_icon_24;
            this.migrateJobsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.migrateJobsButton.Name = "migrateJobsButton";
            this.migrateJobsButton.Size = new System.Drawing.Size(148, 28);
            this.migrateJobsButton.Text = "Migrate selected Jobs";
            this.migrateJobsButton.ToolTipText = "Migrate selected jobs to Target environment";
            this.migrateJobsButton.Click += new System.EventHandler(this.MigrateJobsButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 31);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1215, 531);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.targetEnvLabel);
            this.groupBox1.Controls.Add(this.targetEnvButton);
            this.groupBox1.Controls.Add(this.sourceEnvLabel);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1209, 74);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Environments";
            // 
            // targetEnvLabel
            // 
            this.targetEnvLabel.AutoSize = true;
            this.targetEnvLabel.ForeColor = System.Drawing.Color.Red;
            this.targetEnvLabel.Location = new System.Drawing.Point(107, 45);
            this.targetEnvLabel.Name = "targetEnvLabel";
            this.targetEnvLabel.Size = new System.Drawing.Size(69, 13);
            this.targetEnvLabel.TabIndex = 3;
            this.targetEnvLabel.Text = "Not Selected";
            // 
            // targetEnvButton
            // 
            this.targetEnvButton.Location = new System.Drawing.Point(9, 40);
            this.targetEnvButton.Name = "targetEnvButton";
            this.targetEnvButton.Size = new System.Drawing.Size(83, 23);
            this.targetEnvButton.TabIndex = 2;
            this.targetEnvButton.Text = "Select Target";
            this.targetEnvButton.UseVisualStyleBackColor = true;
            this.targetEnvButton.Click += new System.EventHandler(this.TargetEnvButton_Click);
            // 
            // sourceEnvLabel
            // 
            this.sourceEnvLabel.AutoSize = true;
            this.sourceEnvLabel.ForeColor = System.Drawing.Color.Red;
            this.sourceEnvLabel.Location = new System.Drawing.Point(107, 20);
            this.sourceEnvLabel.Name = "sourceEnvLabel";
            this.sourceEnvLabel.Size = new System.Drawing.Size(69, 13);
            this.sourceEnvLabel.TabIndex = 1;
            this.sourceEnvLabel.Text = "Not Selected";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.logTextBox);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 384);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1209, 144);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Logs";
            // 
            // logTextBox
            // 
            this.logTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextBox.HideSelection = false;
            this.logTextBox.Location = new System.Drawing.Point(3, 16);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTextBox.Size = new System.Drawing.Size(1203, 125);
            this.logTextBox.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.jobsDataGridView);
            this.groupBox3.Controls.Add(this.panel1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 83);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1209, 295);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Bulk Deletion Jobs";
            // 
            // jobsDataGridView
            // 
            this.jobsDataGridView.AllowUserToAddRows = false;
            this.jobsDataGridView.AllowUserToDeleteRows = false;
            this.jobsDataGridView.AllowUserToResizeRows = false;
            this.jobsDataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.jobsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.jobsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.JobSelection,
            this.JobName,
            this.Table,
            this.Frequency,
            this.Interval,
            this.RecurrenceDate,
            this.Status,
            this.StatusReason});
            this.jobsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jobsDataGridView.Location = new System.Drawing.Point(3, 46);
            this.jobsDataGridView.MultiSelect = false;
            this.jobsDataGridView.Name = "jobsDataGridView";
            this.jobsDataGridView.RowHeadersVisible = false;
            this.jobsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.jobsDataGridView.Size = new System.Drawing.Size(1203, 246);
            this.jobsDataGridView.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.selectAllCheckBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1203, 30);
            this.panel1.TabIndex = 0;
            // 
            // selectAllCheckBox
            // 
            this.selectAllCheckBox.AutoSize = true;
            this.selectAllCheckBox.Location = new System.Drawing.Point(6, 7);
            this.selectAllCheckBox.Name = "selectAllCheckBox";
            this.selectAllCheckBox.Size = new System.Drawing.Size(117, 17);
            this.selectAllCheckBox.TabIndex = 0;
            this.selectAllCheckBox.Text = "Select/Unselect All";
            this.selectAllCheckBox.UseVisualStyleBackColor = true;
            this.selectAllCheckBox.CheckedChanged += new System.EventHandler(this.SelectAllCheckBox_CheckedChanged);
            // 
            // JobSelection
            // 
            this.JobSelection.HeaderText = " ";
            this.JobSelection.Name = "JobSelection";
            this.JobSelection.Width = 30;
            // 
            // JobName
            // 
            this.JobName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.JobName.HeaderText = "Name";
            this.JobName.Name = "JobName";
            this.JobName.ReadOnly = true;
            // 
            // Table
            // 
            this.Table.HeaderText = "Table";
            this.Table.Name = "Table";
            this.Table.ReadOnly = true;
            this.Table.Width = 350;
            // 
            // Frequency
            // 
            this.Frequency.HeaderText = "Frequency";
            this.Frequency.Name = "Frequency";
            this.Frequency.ReadOnly = true;
            // 
            // Interval
            // 
            this.Interval.HeaderText = "Interval";
            this.Interval.Name = "Interval";
            this.Interval.ReadOnly = true;
            // 
            // RecurrenceDate
            // 
            this.RecurrenceDate.HeaderText = "Start Time";
            this.RecurrenceDate.Name = "RecurrenceDate";
            this.RecurrenceDate.ReadOnly = true;
            this.RecurrenceDate.Width = 150;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 150;
            // 
            // StatusReason
            // 
            this.StatusReason.HeaderText = "Status Reason";
            this.StatusReason.Name = "StatusReason";
            this.StatusReason.ReadOnly = true;
            this.StatusReason.Width = 150;
            // 
            // BulkDeleteMigrator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "BulkDeleteMigrator";
            this.Size = new System.Drawing.Size(1215, 562);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.jobsDataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripButton loadJobsButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton migrateJobsButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label sourceEnvLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label targetEnvLabel;
        private System.Windows.Forms.Button targetEnvButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView jobsDataGridView;
        private System.Windows.Forms.CheckBox selectAllCheckBox;
        private System.Windows.Forms.DataGridViewCheckBoxColumn JobSelection;
        private System.Windows.Forms.DataGridViewTextBoxColumn JobName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Table;
        private System.Windows.Forms.DataGridViewTextBoxColumn Frequency;
        private System.Windows.Forms.DataGridViewTextBoxColumn Interval;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecurrenceDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusReason;
    }
}
