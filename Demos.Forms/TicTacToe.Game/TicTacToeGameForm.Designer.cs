using Demos.Forms.Base;

namespace Demos.Forms.TicTacToe.Game
{
    partial class TicTacToeGameForm
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
            this.boardControl = new TicTacToeBoardControl();
            this.SuspendLayout();
            // 
            // ticTacToeBoardControl1
            // 
            this.boardControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardControl.Location = new System.Drawing.Point(145, 44);
            this.boardControl.Name = "ticTacToeBoardControl1";
            this.boardControl.Size = new System.Drawing.Size(260, 232);
            this.boardControl.TabIndex = 0;
            this.boardControl.OnAction += new GameActionHandler<global::Games.TicTacToe.GameAction>(this.ticTacToeBoardControl1_OnAction);
            // 
            // TicTacToeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.boardControl);
            this.Name = "TicTacToeForm";
            this.Text = "Tic Tac Toe";
            this.Load += new System.EventHandler(this.TicTacToeForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private TicTacToeBoardControl boardControl;
    }
}