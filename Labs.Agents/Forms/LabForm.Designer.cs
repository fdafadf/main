﻿namespace Labs.Agents.Forms
{
    partial class LabForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LabForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listViewEnvironments = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pictureBoxSpacePreview = new System.Windows.Forms.PictureBox();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.menuItemNewEnvironment = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpenMapDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.agentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNewAgent = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewAgentDrivers = new System.Windows.Forms.ListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.listViewSimulationDefinitions = new System.Windows.Forms.ListView();
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip3 = new System.Windows.Forms.MenuStrip();
            this.menuItemSimulation = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewSimulation = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tabPageSimulationResults = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.listViewSimulationResults = new System.Windows.Forms.ListView();
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuItemShowSimulationResults = new System.Windows.Forms.ToolStripButton();
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSpacePreview)).BeginInit();
            this.menuStrip2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.menuStrip3.SuspendLayout();
            this.tabPageSimulationResults.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPageSimulationResults);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(723, 396);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(715, 370);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Environments";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.menuStrip2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(715, 370);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 27);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listViewEnvironments);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.pictureBoxSpacePreview);
            this.splitContainer1.Size = new System.Drawing.Size(709, 340);
            this.splitContainer1.SplitterDistance = 170;
            this.splitContainer1.TabIndex = 2;
            // 
            // listViewEnvironments
            // 
            this.listViewEnvironments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.listViewEnvironments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewEnvironments.FullRowSelect = true;
            this.listViewEnvironments.HideSelection = false;
            this.listViewEnvironments.Location = new System.Drawing.Point(0, 0);
            this.listViewEnvironments.Name = "listViewEnvironments";
            this.listViewEnvironments.Size = new System.Drawing.Size(709, 170);
            this.listViewEnvironments.TabIndex = 0;
            this.listViewEnvironments.UseCompatibleStateImageBehavior = false;
            this.listViewEnvironments.View = System.Windows.Forms.View.Details;
            this.listViewEnvironments.SelectedIndexChanged += new System.EventHandler(this.listViewEnvironments_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Name";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Width";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Height";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Seed";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Agents";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Obstacles";
            // 
            // pictureBoxSpacePreview
            // 
            this.pictureBoxSpacePreview.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxSpacePreview.Name = "pictureBoxSpacePreview";
            this.pictureBoxSpacePreview.Size = new System.Drawing.Size(226, 72);
            this.pictureBoxSpacePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxSpacePreview.TabIndex = 0;
            this.pictureBoxSpacePreview.TabStop = false;
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemNewEnvironment,
            this.menuItemOpenMapDirectory});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(715, 24);
            this.menuStrip2.TabIndex = 3;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // menuItemNewEnvironment
            // 
            this.menuItemNewEnvironment.Name = "menuItemNewEnvironment";
            this.menuItemNewEnvironment.Size = new System.Drawing.Size(43, 20);
            this.menuItemNewEnvironment.Text = "&New";
            this.menuItemNewEnvironment.Click += new System.EventHandler(this.menuItemNewEnvironment_Click);
            // 
            // menuItemOpenMapDirectory
            // 
            this.menuItemOpenMapDirectory.Name = "menuItemOpenMapDirectory";
            this.menuItemOpenMapDirectory.Size = new System.Drawing.Size(126, 20);
            this.menuItemOpenMapDirectory.Text = "&Open Map Directory";
            this.menuItemOpenMapDirectory.Click += new System.EventHandler(this.menuItemOpenMapDirectory_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tableLayoutPanel2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(715, 370);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Agents";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.menuStrip1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.listViewAgentDrivers, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(715, 370);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.agentToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(715, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // agentToolStripMenuItem
            // 
            this.agentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemNewAgent});
            this.agentToolStripMenuItem.Name = "agentToolStripMenuItem";
            this.agentToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.agentToolStripMenuItem.Text = "&Agent";
            // 
            // menuItemNewAgent
            // 
            this.menuItemNewAgent.Name = "menuItemNewAgent";
            this.menuItemNewAgent.Size = new System.Drawing.Size(98, 22);
            this.menuItemNewAgent.Text = "&New";
            // 
            // listViewAgentDrivers
            // 
            this.listViewAgentDrivers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader13,
            this.columnHeader10});
            this.listViewAgentDrivers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewAgentDrivers.FullRowSelect = true;
            this.listViewAgentDrivers.HideSelection = false;
            this.listViewAgentDrivers.Location = new System.Drawing.Point(3, 27);
            this.listViewAgentDrivers.Name = "listViewAgentDrivers";
            this.listViewAgentDrivers.Size = new System.Drawing.Size(709, 340);
            this.listViewAgentDrivers.TabIndex = 0;
            this.listViewAgentDrivers.UseCompatibleStateImageBehavior = false;
            this.listViewAgentDrivers.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Name";
            this.columnHeader9.Width = 102;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Type";
            this.columnHeader13.Width = 101;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Description";
            this.columnHeader10.Width = 136;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(715, 370);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Simulation Definitions";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.listViewSimulationDefinitions, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.menuStrip3, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(715, 370);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // listViewSimulationDefinitions
            // 
            this.listViewSimulationDefinitions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader12,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader11});
            this.listViewSimulationDefinitions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewSimulationDefinitions.FullRowSelect = true;
            this.listViewSimulationDefinitions.HideSelection = false;
            this.listViewSimulationDefinitions.Location = new System.Drawing.Point(3, 30);
            this.listViewSimulationDefinitions.Name = "listViewSimulationDefinitions";
            this.listViewSimulationDefinitions.Size = new System.Drawing.Size(709, 337);
            this.listViewSimulationDefinitions.TabIndex = 2;
            this.listViewSimulationDefinitions.UseCompatibleStateImageBehavior = false;
            this.listViewSimulationDefinitions.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Name";
            this.columnHeader12.Width = 82;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Environment";
            this.columnHeader1.Width = 141;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Agent";
            this.columnHeader2.Width = 126;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Description";
            this.columnHeader11.Width = 120;
            // 
            // menuStrip3
            // 
            this.menuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSimulation,
            this.toolStripSeparator1});
            this.menuStrip3.Location = new System.Drawing.Point(0, 0);
            this.menuStrip3.Name = "menuStrip3";
            this.menuStrip3.Size = new System.Drawing.Size(715, 27);
            this.menuStrip3.TabIndex = 3;
            this.menuStrip3.Text = "menuStrip3";
            // 
            // menuItemSimulation
            // 
            this.menuItemSimulation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNewSimulation});
            this.menuItemSimulation.Name = "menuItemSimulation";
            this.menuItemSimulation.Size = new System.Drawing.Size(76, 23);
            this.menuItemSimulation.Text = "&Simulation";
            // 
            // menuNewSimulation
            // 
            this.menuNewSimulation.Name = "menuNewSimulation";
            this.menuNewSimulation.Size = new System.Drawing.Size(98, 22);
            this.menuNewSimulation.Text = "&New";
            this.menuNewSimulation.Click += new System.EventHandler(this.menuNewSimulation_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // tabPageSimulationResults
            // 
            this.tabPageSimulationResults.Controls.Add(this.tableLayoutPanel4);
            this.tabPageSimulationResults.Location = new System.Drawing.Point(4, 22);
            this.tabPageSimulationResults.Name = "tabPageSimulationResults";
            this.tabPageSimulationResults.Size = new System.Drawing.Size(715, 370);
            this.tabPageSimulationResults.TabIndex = 3;
            this.tabPageSimulationResults.Text = "Simulation Results";
            this.tabPageSimulationResults.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.listViewSimulationResults, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(715, 370);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemShowSimulationResults});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(715, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // listViewSimulationResults
            // 
            this.listViewSimulationResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader14,
            this.columnHeader16,
            this.columnHeader15,
            this.columnHeader17});
            this.listViewSimulationResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewSimulationResults.FullRowSelect = true;
            this.listViewSimulationResults.HideSelection = false;
            this.listViewSimulationResults.Location = new System.Drawing.Point(3, 28);
            this.listViewSimulationResults.Name = "listViewSimulationResults";
            this.listViewSimulationResults.Size = new System.Drawing.Size(709, 339);
            this.listViewSimulationResults.TabIndex = 1;
            this.listViewSimulationResults.UseCompatibleStateImageBehavior = false;
            this.listViewSimulationResults.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Date";
            this.columnHeader14.Width = 135;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Agent";
            this.columnHeader16.Width = 95;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Simulation";
            this.columnHeader15.Width = 93;
            // 
            // menuItemShowSimulationResults
            // 
            this.menuItemShowSimulationResults.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuItemShowSimulationResults.Image = ((System.Drawing.Image)(resources.GetObject("menuItemShowSimulationResults.Image")));
            this.menuItemShowSimulationResults.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuItemShowSimulationResults.Name = "menuItemShowSimulationResults";
            this.menuItemShowSimulationResults.Size = new System.Drawing.Size(40, 22);
            this.menuItemShowSimulationResults.Text = "Show";
            this.menuItemShowSimulationResults.Click += new System.EventHandler(this.menuItemShowSimulationResults_Click);
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "Length";
            // 
            // LabForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 396);
            this.Controls.Add(this.tabControl1);
            this.MainMenuStrip = this.menuStrip3;
            this.Name = "LabForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agents Lab";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSpacePreview)).EndInit();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.menuStrip3.ResumeLayout(false);
            this.menuStrip3.PerformLayout();
            this.tabPageSimulationResults.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView listViewSimulationDefinitions;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPageSimulationResults;
        private System.Windows.Forms.ListView listViewAgentDrivers;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listViewEnvironments;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.PictureBox pictureBoxSpacePreview;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem menuItemNewEnvironment;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.MenuStrip menuStrip3;
        private System.Windows.Forms.ToolStripMenuItem menuItemSimulation;
        private System.Windows.Forms.ToolStripMenuItem agentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemNewAgent;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuNewSimulation;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpenMapDirectory;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ListView listViewSimulationResults;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ToolStripButton menuItemShowSimulationResults;
        private System.Windows.Forms.ColumnHeader columnHeader17;
    }
}