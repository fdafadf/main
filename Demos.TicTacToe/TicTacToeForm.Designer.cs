namespace Demos.TicTacToe
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxLayers = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericEpoches = new System.Windows.Forms.NumericUpDown();
            this.buttonCreateAndTrain = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.progressBarTraining = new System.Windows.Forms.ProgressBar();
            this.buttonTrained = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelMeanSquareError = new System.Windows.Forms.Label();
            this.labelEpoch = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelAccuracy = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonCircle = new System.Windows.Forms.Button();
            this.buttonCross = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.boardControl = new Demos.Forms.TicTacToe.TicTacToeBoardControl();
            this.workerTrain = new System.ComponentModel.BackgroundWorker();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numericLearningRate = new System.Windows.Forms.NumericUpDown();
            this.numericMomentum = new System.Windows.Forms.NumericUpDown();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericEpoches)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericLearningRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMomentum)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(582, 310);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(574, 301);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.textBoxLayers, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.numericEpoches, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonCreateAndTrain, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.numericLearningRate, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.numericMomentum, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(568, 295);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // textBoxLayers
            // 
            this.textBoxLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLayers.Location = new System.Drawing.Point(119, 72);
            this.textBoxLayers.Name = "textBoxLayers";
            this.textBoxLayers.Size = new System.Drawing.Size(446, 26);
            this.textBoxLayers.TabIndex = 1;
            this.textBoxLayers.Text = "72, 72, 72, 36, 36, 36, 18, 18";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Layers";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "Epoches";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericEpoches
            // 
            this.numericEpoches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericEpoches.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericEpoches.Location = new System.Drawing.Point(119, 104);
            this.numericEpoches.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericEpoches.Name = "numericEpoches";
            this.numericEpoches.Size = new System.Drawing.Size(446, 26);
            this.numericEpoches.TabIndex = 2;
            this.numericEpoches.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // buttonCreateAndTrain
            // 
            this.buttonCreateAndTrain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCreateAndTrain.Location = new System.Drawing.Point(455, 269);
            this.buttonCreateAndTrain.Name = "buttonCreateAndTrain";
            this.buttonCreateAndTrain.Size = new System.Drawing.Size(110, 23);
            this.buttonCreateAndTrain.TabIndex = 3;
            this.buttonCreateAndTrain.Text = "Create and Train";
            this.buttonCreateAndTrain.UseVisualStyleBackColor = true;
            this.buttonCreateAndTrain.Click += new System.EventHandler(this.buttonCreateAndTrain_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(574, 301);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.progressBarTraining, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.buttonTrained, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.labelMeanSquareError, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.labelEpoch, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.labelAccuracy, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 8;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(568, 295);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // progressBarTraining
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.progressBarTraining, 2);
            this.progressBarTraining.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBarTraining.Location = new System.Drawing.Point(3, 180);
            this.progressBarTraining.Name = "progressBarTraining";
            this.progressBarTraining.Size = new System.Drawing.Size(562, 23);
            this.progressBarTraining.TabIndex = 1;
            // 
            // buttonTrained
            // 
            this.buttonTrained.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTrained.AutoSize = true;
            this.buttonTrained.Location = new System.Drawing.Point(490, 268);
            this.buttonTrained.Name = "buttonTrained";
            this.buttonTrained.Size = new System.Drawing.Size(75, 23);
            this.buttonTrained.TabIndex = 0;
            this.buttonTrained.Text = "Continue";
            this.buttonTrained.UseVisualStyleBackColor = true;
            this.buttonTrained.Click += new System.EventHandler(this.buttonTrained_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(278, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Accuracy";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(278, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Mean Square Error";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelMeanSquareError
            // 
            this.labelMeanSquareError.AutoSize = true;
            this.labelMeanSquareError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelMeanSquareError.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMeanSquareError.Location = new System.Drawing.Point(287, 78);
            this.labelMeanSquareError.Name = "labelMeanSquareError";
            this.labelMeanSquareError.Size = new System.Drawing.Size(278, 20);
            this.labelMeanSquareError.TabIndex = 3;
            this.labelMeanSquareError.Text = "label5";
            this.labelMeanSquareError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelEpoch
            // 
            this.labelEpoch.AutoSize = true;
            this.labelEpoch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelEpoch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEpoch.Location = new System.Drawing.Point(287, 98);
            this.labelEpoch.Name = "labelEpoch";
            this.labelEpoch.Size = new System.Drawing.Size(278, 20);
            this.labelEpoch.TabIndex = 4;
            this.labelEpoch.Text = "label5";
            this.labelEpoch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(278, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Epoch";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelAccuracy
            // 
            this.labelAccuracy.AutoSize = true;
            this.labelAccuracy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAccuracy.Location = new System.Drawing.Point(287, 58);
            this.labelAccuracy.Name = "labelAccuracy";
            this.labelAccuracy.Size = new System.Drawing.Size(51, 20);
            this.labelAccuracy.TabIndex = 6;
            this.labelAccuracy.Text = "label5";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tableLayoutPanel3);
            this.tabPage3.Location = new System.Drawing.Point(4, 5);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(574, 301);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.buttonCircle, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.buttonCross, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(568, 295);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // buttonCircle
            // 
            this.buttonCircle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCircle.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCircle.Location = new System.Drawing.Point(3, 23);
            this.buttonCircle.Name = "buttonCircle";
            this.buttonCircle.Size = new System.Drawing.Size(278, 269);
            this.buttonCircle.TabIndex = 1;
            this.buttonCircle.Text = "◯";
            this.buttonCircle.UseVisualStyleBackColor = true;
            this.buttonCircle.Click += new System.EventHandler(this.buttonCircle_Click);
            // 
            // buttonCross
            // 
            this.buttonCross.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCross.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCross.Location = new System.Drawing.Point(287, 23);
            this.buttonCross.Name = "buttonCross";
            this.buttonCross.Size = new System.Drawing.Size(278, 269);
            this.buttonCross.TabIndex = 0;
            this.buttonCross.Text = "╳";
            this.buttonCross.UseVisualStyleBackColor = true;
            this.buttonCross.Click += new System.EventHandler(this.buttonCross_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.label5, 2);
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(562, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "Play";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.boardControl);
            this.tabPage4.Location = new System.Drawing.Point(4, 5);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(574, 301);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // boardControl
            // 
            this.boardControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boardControl.Location = new System.Drawing.Point(3, 3);
            this.boardControl.Name = "boardControl";
            this.boardControl.Size = new System.Drawing.Size(568, 295);
            this.boardControl.TabIndex = 2;
            this.boardControl.OnAction += new Demos.Forms.Base.GameActionHandler<Games.TicTacToe.GameAction>(this.boardControl_OnAction);
            // 
            // workerTrain
            // 
            this.workerTrain.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerTrain_DoWorkAsync);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 32);
            this.label7.TabIndex = 4;
            this.label7.Text = "Learning Rate";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 165);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 32);
            this.label8.TabIndex = 5;
            this.label8.Text = "Momentum";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericLearningRate
            // 
            this.numericLearningRate.DecimalPlaces = 4;
            this.numericLearningRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericLearningRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericLearningRate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericLearningRate.Location = new System.Drawing.Point(119, 136);
            this.numericLearningRate.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericLearningRate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.numericLearningRate.Name = "numericLearningRate";
            this.numericLearningRate.Size = new System.Drawing.Size(446, 26);
            this.numericLearningRate.TabIndex = 6;
            this.numericLearningRate.Value = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            // 
            // numericMomentum
            // 
            this.numericMomentum.DecimalPlaces = 4;
            this.numericMomentum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericMomentum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericMomentum.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericMomentum.Location = new System.Drawing.Point(119, 168);
            this.numericMomentum.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericMomentum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.numericMomentum.Name = "numericMomentum";
            this.numericMomentum.Size = new System.Drawing.Size(446, 26);
            this.numericMomentum.TabIndex = 7;
            this.numericMomentum.Value = new decimal(new int[] {
            4,
            0,
            0,
            131072});
            // 
            // TicTacToeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 310);
            this.Controls.Add(this.tabControl1);
            this.Name = "TicTacToeForm";
            this.Text = "TicTacToe";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericEpoches)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericLearningRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMomentum)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox textBoxLayers;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericEpoches;
        private System.Windows.Forms.Button buttonCreateAndTrain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ProgressBar progressBarTraining;
        private System.Windows.Forms.Button buttonTrained;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.ComponentModel.BackgroundWorker workerTrain;
        private System.Windows.Forms.Label labelMeanSquareError;
        private System.Windows.Forms.Label labelEpoch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelAccuracy;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button buttonCircle;
        private System.Windows.Forms.Button buttonCross;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPage4;
        private Demos.Forms.TicTacToe.TicTacToeBoardControl boardControl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericLearningRate;
        private System.Windows.Forms.NumericUpDown numericMomentum;
    }
}

