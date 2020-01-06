namespace Demos.Forms.Go.Game
{
    partial class GoGameForm
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
            this.button2 = new System.Windows.Forms.Button();
            this.preparedPositionsControl = new System.Windows.Forms.ComboBox();
            this.mainBoardNavigationScroll = new System.Windows.Forms.HScrollBar();
            this.pathsControl = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numberOfPlayoutsControl = new System.Windows.Forms.NumericUpDown();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.goBoardControl1 = new Demos.Forms.Go.Game.GoBoardControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.playoutScrollBar = new System.Windows.Forms.HScrollBar();
            this.playoutListControl = new System.Windows.Forms.ListBox();
            this.playoutBoardControl = new Demos.Forms.Go.Game.GoBoardControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfPlayoutsControl)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 45);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Generate Playouts";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // preparedPositionsControl
            // 
            this.preparedPositionsControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.preparedPositionsControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.preparedPositionsControl.FormattingEnabled = true;
            this.preparedPositionsControl.Location = new System.Drawing.Point(3, 16);
            this.preparedPositionsControl.Name = "preparedPositionsControl";
            this.preparedPositionsControl.Size = new System.Drawing.Size(156, 21);
            this.preparedPositionsControl.TabIndex = 3;
            // 
            // mainBoardNavigationScroll
            // 
            this.mainBoardNavigationScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainBoardNavigationScroll.LargeChange = 1;
            this.mainBoardNavigationScroll.Location = new System.Drawing.Point(0, 330);
            this.mainBoardNavigationScroll.Name = "mainBoardNavigationScroll";
            this.mainBoardNavigationScroll.Size = new System.Drawing.Size(548, 17);
            this.mainBoardNavigationScroll.TabIndex = 5;
            // 
            // pathsControl
            // 
            this.pathsControl.FormattingEnabled = true;
            this.pathsControl.Location = new System.Drawing.Point(571, 49);
            this.pathsControl.Name = "pathsControl";
            this.pathsControl.Size = new System.Drawing.Size(121, 21);
            this.pathsControl.TabIndex = 6;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(73, 20);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(83, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "With Details";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.numberOfPlayoutsControl);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(571, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(162, 87);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Monte Carlo";
            // 
            // numberOfPlayoutsControl
            // 
            this.numberOfPlayoutsControl.Location = new System.Drawing.Point(6, 19);
            this.numberOfPlayoutsControl.Name = "numberOfPlayoutsControl";
            this.numberOfPlayoutsControl.Size = new System.Drawing.Size(61, 20);
            this.numberOfPlayoutsControl.TabIndex = 8;
            this.numberOfPlayoutsControl.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tableLayoutPanel1.SetRowSpan(this.tabControl1, 4);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(562, 379);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(554, 353);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Board";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.mainBoardNavigationScroll, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.goBoardControl1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(548, 347);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // goBoardControl1
            // 
            this.goBoardControl1.BackColor = System.Drawing.Color.Cornsilk;
            this.goBoardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.goBoardControl1.Location = new System.Drawing.Point(3, 3);
            this.goBoardControl1.Name = "goBoardControl1";
            this.goBoardControl1.Size = new System.Drawing.Size(542, 324);
            this.goBoardControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(554, 353);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Playouts";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.playoutScrollBar, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.playoutListControl, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.playoutBoardControl, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(548, 347);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // playoutScrollBar
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.playoutScrollBar, 2);
            this.playoutScrollBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playoutScrollBar.Location = new System.Drawing.Point(0, 330);
            this.playoutScrollBar.Name = "playoutScrollBar";
            this.playoutScrollBar.Size = new System.Drawing.Size(548, 17);
            this.playoutScrollBar.TabIndex = 2;
            // 
            // playoutListControl
            // 
            this.playoutListControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playoutListControl.FormattingEnabled = true;
            this.playoutListControl.Location = new System.Drawing.Point(417, 3);
            this.playoutListControl.Name = "playoutListControl";
            this.playoutListControl.Size = new System.Drawing.Size(128, 324);
            this.playoutListControl.TabIndex = 3;
            this.playoutListControl.SelectedIndexChanged += new System.EventHandler(this.playoutListControl_SelectedIndexChanged);
            // 
            // playoutBoardControl
            // 
            this.playoutBoardControl.BackColor = System.Drawing.Color.Cornsilk;
            this.playoutBoardControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playoutBoardControl.Location = new System.Drawing.Point(3, 3);
            this.playoutBoardControl.Name = "playoutBoardControl";
            this.playoutBoardControl.Size = new System.Drawing.Size(408, 324);
            this.playoutBoardControl.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pathsControl, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(736, 385);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.preparedPositionsControl);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(571, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(162, 40);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Predefined Positions";
            // 
            // GoGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 385);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "GoGameForm";
            this.Text = "GoGameForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfPlayoutsControl)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GoBoardControl goBoardControl1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox preparedPositionsControl;
        private System.Windows.Forms.HScrollBar mainBoardNavigationScroll;
        private System.Windows.Forms.ComboBox pathsControl;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numberOfPlayoutsControl;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.HScrollBar playoutScrollBar;
        private GoBoardControl playoutBoardControl;
        private System.Windows.Forms.ListBox playoutListControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    }
}