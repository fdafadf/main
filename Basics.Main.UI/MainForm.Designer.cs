namespace Basics.Main.UI
{
    partial class MainForm
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
            this.newPerceptronDemoButton = new System.Windows.Forms.ToolStripMenuItem();
            this.ticTacToePerceptronButton = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newPerceptronDemoButton,
            this.ticTacToePerceptronButton});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(43, 20);
            this.toolStripMenuItem1.Text = "&New";
            // 
            // newPerceptronDemoButton
            // 
            this.newPerceptronDemoButton.Name = "newPerceptronDemoButton";
            this.newPerceptronDemoButton.Size = new System.Drawing.Size(191, 22);
            this.newPerceptronDemoButton.Text = "&Perceptron Demo";
            this.newPerceptronDemoButton.Click += new System.EventHandler(this.newPerceptronDemoButton_Click);
            // 
            // ticTacToePerceptronButton
            // 
            this.ticTacToePerceptronButton.Name = "ticTacToePerceptronButton";
            this.ticTacToePerceptronButton.Size = new System.Drawing.Size(191, 22);
            this.ticTacToePerceptronButton.Text = "&Tic Tac Toe Perceptron";
            this.ticTacToePerceptronButton.Click += new System.EventHandler(this.ticTacToePerceptronButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newPerceptronDemoButton;
        private System.Windows.Forms.ToolStripMenuItem ticTacToePerceptronButton;
    }
}