namespace Basics.AI.NeuralNetworks.Demos.PerceptronTicTacToe
{
    partial class TicTacToePerceptronForm
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
            this.loadButton = new System.Windows.Forms.Button();
            this.perceptronsControl = new System.Windows.Forms.ComboBox();
            this.trainDataSetsControl = new System.Windows.Forms.ComboBox();
            this.trainButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.boardControl = new Basics.AI.NeuralNetworks.Demos.PerceptronTicTacToe.PerceptronTicTacToeBoardControl();
            this.randomButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(523, 65);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 23);
            this.loadButton.TabIndex = 1;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // perceptronsControl
            // 
            this.perceptronsControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.perceptronsControl.FormattingEnabled = true;
            this.perceptronsControl.Location = new System.Drawing.Point(485, 34);
            this.perceptronsControl.Name = "perceptronsControl";
            this.perceptronsControl.Size = new System.Drawing.Size(249, 21);
            this.perceptronsControl.TabIndex = 2;
            // 
            // trainDataSetsControl
            // 
            this.trainDataSetsControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.trainDataSetsControl.FormattingEnabled = true;
            this.trainDataSetsControl.Location = new System.Drawing.Point(491, 151);
            this.trainDataSetsControl.Name = "trainDataSetsControl";
            this.trainDataSetsControl.Size = new System.Drawing.Size(254, 21);
            this.trainDataSetsControl.TabIndex = 3;
            // 
            // trainButton
            // 
            this.trainButton.Location = new System.Drawing.Point(548, 187);
            this.trainButton.Name = "trainButton";
            this.trainButton.Size = new System.Drawing.Size(75, 23);
            this.trainButton.TabIndex = 4;
            this.trainButton.Text = "Train";
            this.trainButton.UseVisualStyleBackColor = true;
            this.trainButton.Click += new System.EventHandler(this.trainButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(527, 99);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // boardControl
            // 
            this.boardControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardControl.Location = new System.Drawing.Point(59, 60);
            this.boardControl.Name = "boardControl";
            this.boardControl.Size = new System.Drawing.Size(287, 253);
            this.boardControl.TabIndex = 0;
            this.boardControl.OnAction += new Basics.Games.Forms.GameActionHandler<Basics.Games.TicTacToe.GameAction>(this.perceptronTicTacToeBoardControl1_OnAction);
            // 
            // randomButton
            // 
            this.randomButton.Location = new System.Drawing.Point(662, 193);
            this.randomButton.Name = "randomButton";
            this.randomButton.Size = new System.Drawing.Size(75, 23);
            this.randomButton.TabIndex = 6;
            this.randomButton.Text = "Random";
            this.randomButton.UseVisualStyleBackColor = true;
            this.randomButton.Click += new System.EventHandler(this.randomButton_Click);
            // 
            // TicTacToePerceptronForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.randomButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.trainButton);
            this.Controls.Add(this.trainDataSetsControl);
            this.Controls.Add(this.perceptronsControl);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.boardControl);
            this.Name = "TicTacToePerceptronForm";
            this.Text = "TicTacToePerceptronForm";
            this.Load += new System.EventHandler(this.TicTacToePerceptronForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private PerceptronTicTacToeBoardControl boardControl;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.ComboBox perceptronsControl;
        private System.Windows.Forms.ComboBox trainDataSetsControl;
        private System.Windows.Forms.Button trainButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button randomButton;
    }
}