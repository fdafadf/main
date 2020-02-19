namespace Demo
{
    partial class DemoMainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.percToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linearFunctionSolverMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.neuralNetworksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.neuralXorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.neuralTicTacToeTMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monteCarloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ticTacToeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ticTacToeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monteCarloTrainerDemoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.percToolStripMenuItem,
            this.neuralNetworksToolStripMenuItem,
            this.monteCarloToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(56, 20);
            this.toolStripMenuItem1.Text = "&Demos";
            // 
            // percToolStripMenuItem
            // 
            this.percToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.linearFunctionSolverMenuItem});
            this.percToolStripMenuItem.Name = "percToolStripMenuItem";
            this.percToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.percToolStripMenuItem.Text = "&Perceptron";
            // 
            // linearFunctionSolverMenuItem
            // 
            this.linearFunctionSolverMenuItem.Name = "linearFunctionSolverMenuItem";
            this.linearFunctionSolverMenuItem.Size = new System.Drawing.Size(191, 22);
            this.linearFunctionSolverMenuItem.Text = "&Linear Function Solver";
            this.linearFunctionSolverMenuItem.Click += new System.EventHandler(this.linearFunctionSolverMenuItem_Click);
            // 
            // neuralNetworksToolStripMenuItem
            // 
            this.neuralNetworksToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.neuralXorMenuItem,
            this.neuralTicTacToeTMenuItem,
            this.monteCarloTrainerDemoMenuItem});
            this.neuralNetworksToolStripMenuItem.Name = "neuralNetworksToolStripMenuItem";
            this.neuralNetworksToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.neuralNetworksToolStripMenuItem.Text = "&Neural Networks";
            // 
            // neuralXorMenuItem
            // 
            this.neuralXorMenuItem.Name = "neuralXorMenuItem";
            this.neuralXorMenuItem.Size = new System.Drawing.Size(180, 22);
            this.neuralXorMenuItem.Text = "&XOR";
            this.neuralXorMenuItem.Click += new System.EventHandler(this.neuralXorMenuItem_Click);
            // 
            // neuralTicTacToeTMenuItem
            // 
            this.neuralTicTacToeTMenuItem.Name = "neuralTicTacToeTMenuItem";
            this.neuralTicTacToeTMenuItem.Size = new System.Drawing.Size(180, 22);
            this.neuralTicTacToeTMenuItem.Text = "&Tic Tac Toe";
            this.neuralTicTacToeTMenuItem.Click += new System.EventHandler(this.neuralTicTacToeTMenuItem_Click);
            // 
            // monteCarloToolStripMenuItem
            // 
            this.monteCarloToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ticTacToeToolStripMenuItem1});
            this.monteCarloToolStripMenuItem.Name = "monteCarloToolStripMenuItem";
            this.monteCarloToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.monteCarloToolStripMenuItem.Text = "&Monte Carlo";
            // 
            // ticTacToeToolStripMenuItem1
            // 
            this.ticTacToeToolStripMenuItem1.Name = "ticTacToeToolStripMenuItem1";
            this.ticTacToeToolStripMenuItem1.Size = new System.Drawing.Size(127, 22);
            this.ticTacToeToolStripMenuItem1.Text = "&Tic TacToe";
            this.ticTacToeToolStripMenuItem1.Click += new System.EventHandler(this.ticTacToeToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ticTacToeToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(55, 20);
            this.toolStripMenuItem2.Text = "&Games";
            // 
            // ticTacToeToolStripMenuItem
            // 
            this.ticTacToeToolStripMenuItem.Name = "ticTacToeToolStripMenuItem";
            this.ticTacToeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ticTacToeToolStripMenuItem.Text = "&Tic Tac Toe";
            this.ticTacToeToolStripMenuItem.Click += new System.EventHandler(this.ticTacToeToolStripMenuItem_Click);
            // 
            // monteCarloTrainerDemoMenuItem
            // 
            this.monteCarloTrainerDemoMenuItem.Name = "monteCarloTrainerDemoMenuItem";
            this.monteCarloTrainerDemoMenuItem.Size = new System.Drawing.Size(180, 22);
            this.monteCarloTrainerDemoMenuItem.Text = "&Monte Carlo Trainer";
            this.monteCarloTrainerDemoMenuItem.Click += new System.EventHandler(this.monteCarloTrainerDemoMenuItem_Click);
            // 
            // DemoMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DemoMainForm";
            this.Text = "Basics";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem percToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linearFunctionSolverMenuItem;
        private System.Windows.Forms.ToolStripMenuItem neuralNetworksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem neuralXorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem neuralTicTacToeTMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem ticTacToeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monteCarloToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ticTacToeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem monteCarloTrainerDemoMenuItem;
    }
}