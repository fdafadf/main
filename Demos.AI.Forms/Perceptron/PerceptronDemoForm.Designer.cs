namespace Demos.Forms.Perceptron
{
    partial class PerceptronDemoForm
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
            this.train = new System.Windows.Forms.Button();
            this.activationFunctionControl = new Demos.Forms.Utilities.ActivationFunctionControl();
            this.button1 = new System.Windows.Forms.Button();
            this.learningGammaControl = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.learningGammaControl)).BeginInit();
            this.SuspendLayout();
            // 
            // train
            // 
            this.train.Location = new System.Drawing.Point(462, 211);
            this.train.Name = "train";
            this.train.Size = new System.Drawing.Size(94, 23);
            this.train.TabIndex = 0;
            this.train.Text = "Train";
            this.train.UseVisualStyleBackColor = true;
            this.train.Click += new System.EventHandler(this.drawAndTrain_Click);
            // 
            // activationFunctionControl
            // 
            this.activationFunctionControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.activationFunctionControl.FormattingEnabled = true;
            this.activationFunctionControl.Location = new System.Drawing.Point(366, 46);
            this.activationFunctionControl.Name = "activationFunctionControl";
            this.activationFunctionControl.Size = new System.Drawing.Size(243, 21);
            this.activationFunctionControl.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(455, 97);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "New Random Perceptron";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // learningGammaControl
            // 
            this.learningGammaControl.DecimalPlaces = 4;
            this.learningGammaControl.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.learningGammaControl.Location = new System.Drawing.Point(454, 151);
            this.learningGammaControl.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.learningGammaControl.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.learningGammaControl.Name = "learningGammaControl";
            this.learningGammaControl.Size = new System.Drawing.Size(120, 20);
            this.learningGammaControl.TabIndex = 3;
            this.learningGammaControl.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // PerceptronDemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 295);
            this.Controls.Add(this.learningGammaControl);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.activationFunctionControl);
            this.Controls.Add(this.train);
            this.Name = "PerceptronDemoForm";
            this.Text = "Perceptron Demo";
            this.Load += new System.EventHandler(this.PerceptronDemoForm_Load);
            this.DoubleClick += new System.EventHandler(this.PerceptronForm_DoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.learningGammaControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button train;
        private Demos.Forms.Utilities.ActivationFunctionControl activationFunctionControl;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown learningGammaControl;
    }
}

