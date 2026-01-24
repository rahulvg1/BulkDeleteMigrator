namespace BulkDeleteMigrator
{
    partial class MyPluginControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyPluginControl));
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.loadJobsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.migrateJobsButton = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.targetEnvLabel = new System.Windows.Forms.Label();
            this.targetEnvButton = new System.Windows.Forms.Button();
            this.currentEnvLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.jobsDataGridView = new System.Windows.Forms.DataGridView();
            this.JobSelection = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.JobName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Table = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Frequency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Interval = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecurrenceDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.toolStripMenu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.jobsDataGridView)).BeginInit();
            this.groupBox2.SuspendLayout();
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
            this.toolStripMenu.Size = new System.Drawing.Size(1215, 25);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // loadJobsButton
            // 
            this.loadJobsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loadJobsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadJobsButton.Name = "loadJobsButton";
            this.loadJobsButton.Size = new System.Drawing.Size(135, 22);
            this.loadJobsButton.Text = "Reload Bulk Delete Jobs";
            this.loadJobsButton.Click += new System.EventHandler(this.LoadJobsButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // migrateJobsButton
            // 
            this.migrateJobsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.migrateJobsButton.Image = ((System.Drawing.Image)(resources.GetObject("migrateJobsButton.Image")));
            this.migrateJobsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.migrateJobsButton.Name = "migrateJobsButton";
            this.migrateJobsButton.Size = new System.Drawing.Size(125, 22);
            this.migrateJobsButton.Text = "Migrate Selected Jobs";
            this.migrateJobsButton.ToolTipText = "Migrate selected jobs to Target environment";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.jobsDataGridView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1215, 537);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.targetEnvLabel);
            this.groupBox1.Controls.Add(this.targetEnvButton);
            this.groupBox1.Controls.Add(this.currentEnvLabel);
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
            this.targetEnvLabel.Location = new System.Drawing.Point(107, 45);
            this.targetEnvLabel.Name = "targetEnvLabel";
            this.targetEnvLabel.Size = new System.Drawing.Size(137, 13);
            this.targetEnvLabel.TabIndex = 3;
            this.targetEnvLabel.Text = "Environment Name (Target)";
            // 
            // targetEnvButton
            // 
            this.targetEnvButton.Location = new System.Drawing.Point(6, 40);
            this.targetEnvButton.Name = "targetEnvButton";
            this.targetEnvButton.Size = new System.Drawing.Size(83, 23);
            this.targetEnvButton.TabIndex = 2;
            this.targetEnvButton.Text = "Select Target";
            this.targetEnvButton.UseVisualStyleBackColor = true;
            // 
            // currentEnvLabel
            // 
            this.currentEnvLabel.AutoSize = true;
            this.currentEnvLabel.Location = new System.Drawing.Point(107, 20);
            this.currentEnvLabel.Name = "currentEnvLabel";
            this.currentEnvLabel.Size = new System.Drawing.Size(140, 13);
            this.currentEnvLabel.TabIndex = 1;
            this.currentEnvLabel.Text = "Environment Name (Current)";
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
            // jobsDataGridView
            // 
            this.jobsDataGridView.AllowUserToAddRows = false;
            this.jobsDataGridView.AllowUserToDeleteRows = false;
            this.jobsDataGridView.AllowUserToResizeRows = false;
            this.jobsDataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.jobsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.jobsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.JobSelection,
            this.JobName,
            this.Table,
            this.Frequency,
            this.Interval,
            this.RecurrenceDate,
            this.Status});
            this.jobsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jobsDataGridView.Location = new System.Drawing.Point(3, 83);
            this.jobsDataGridView.Name = "jobsDataGridView";
            this.jobsDataGridView.ReadOnly = true;
            this.jobsDataGridView.RowHeadersVisible = false;
            this.jobsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.jobsDataGridView.Size = new System.Drawing.Size(1209, 301);
            this.jobsDataGridView.TabIndex = 1;
            // 
            // JobSelection
            // 
            this.JobSelection.HeaderText = " ";
            this.JobSelection.Name = "JobSelection";
            this.JobSelection.ReadOnly = true;
            this.JobSelection.Width = 50;
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
            this.Table.Width = 300;
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 390);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1209, 144);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Logs";
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(3, 16);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1203, 125);
            this.textBox1.TabIndex = 0;
            // 
            // MyPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "MyPluginControl";
            this.Size = new System.Drawing.Size(1215, 562);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.jobsDataGridView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.Label currentEnvLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label targetEnvLabel;
        private System.Windows.Forms.Button targetEnvButton;
        private System.Windows.Forms.DataGridView jobsDataGridView;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn JobSelection;
        private System.Windows.Forms.DataGridViewTextBoxColumn JobName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Table;
        private System.Windows.Forms.DataGridViewTextBoxColumn Frequency;
        private System.Windows.Forms.DataGridViewTextBoxColumn Interval;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecurrenceDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
    }
}
