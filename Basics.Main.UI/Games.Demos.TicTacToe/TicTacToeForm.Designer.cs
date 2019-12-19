namespace Basics.Games.Demos.TicTacToe
{
    partial class TicTacToeForm
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
            this.ticTacToeBoardControl1 = new Basics.Games.Demos.TicTacToe.TicTacToeBoardControl<TicTacToeBoardFieldControl>();
            this.SuspendLayout();
            // 
            // ticTacToeBoardControl1
            // 
            this.ticTacToeBoardControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ticTacToeBoardControl1.Location = new System.Drawing.Point(145, 44);
            this.ticTacToeBoardControl1.Name = "ticTacToeBoardControl1";
            this.ticTacToeBoardControl1.Size = new System.Drawing.Size(260, 232);
            this.ticTacToeBoardControl1.TabIndex = 0;
            this.ticTacToeBoardControl1.OnAction += new Basics.Games.Forms.GameActionHandler<Basics.Games.TicTacToe.GameAction>(this.ticTacToeBoardControl1_OnAction);
            // 
            // TicTacToeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ticTacToeBoardControl1);
            this.Name = "TicTacToeForm";
            this.Text = "TicTacToeForm";
            this.ResumeLayout(false);

        }

        #endregion

        private TicTacToeBoardControl<TicTacToeBoardFieldControl> ticTacToeBoardControl1;
    }
}