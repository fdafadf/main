using Basics.AI.NeuralNetworks.Demos.PerceptronTicTacToe;
using Basics.Games.TicTacToe;
using Basics.Main.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Basics.AI.NeuralNetworks.Demos.NeuralTicTacToe
{
    public partial class TicTacToeNeuralNetworkDemoForm : Form
    {
        private ComboBox trainDataSetsControl;
        private Button trainButton;
        private Button button1;
        private SaveFileDialog saveFileDialog1;
        private OpenFileDialog openFileDialog1;
        private Button button2;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel1;
        private TicTacToeNeuralBoardControl panel1;

        public TicTacToeNeuralNetworkDemoForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GameState gameState = GameState.Parse("...\n...\n...");
            NeuralIO trainData = new TicTacToeNeuralIO(gameState, new double[] { 0, 0, 0, 1, 0, 0, 0, 0, 0 });
            //GameState gameState = GameState.Parse("...\n.O.\n...");
            //NeuralIO trainData = new TicTacToeNeuralIO(gameState, new double[] { 0, 0, 0, 1, 0, 0, 0, 0, 0 });
            //panel1.NeuralNetwork.Train(new[] { trainData }, 1000);
            //panel1.BoardState = gameState;
            //panel1.Refresh();
        }

        private void TicTacToeNeuralNetworkDemoForm_Load(object sender, EventArgs e)
        {
            string workspacePath = ConfigurationManager.AppSettings["WorkspacePath"];
            string trainDataDirectoryPath = Path.Combine(workspacePath, "TicTacToe.NeuralNetwork", "TrainData");
            trainDataSetsControl.SetItems(new DirectoryInfo(trainDataDirectoryPath).EnumerateFiles());
            trainDataSetsControl.SelectedIndex = 0;
            saveFileDialog1.InitialDirectory = Path.Combine(workspacePath, "TicTacToe.NeuralNetwork", "Networks");
            openFileDialog1.InitialDirectory = Path.Combine(workspacePath, "TicTacToe.NeuralNetwork", "Networks");
        }

        private void trainButton_Click(object sender, EventArgs e)
        {
            var trainData = TicTacToeNeuralIO.Load(trainDataSetsControl.SelectedItem as FileInfo);
            panel1.NeuralNetwork.Train(trainData, 1000);
            panel1.BoardState = panel1.GameState;
            panel1.Refresh();
        }

        private void panel1_OnAction(GameAction action)
        {
            panel1.BoardState = panel1.Game.Play(panel1.GameState, action);
            panel1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileContent = File.ReadAllText(openFileDialog1.FileName);
                double[][][] weights = JsonConvert.DeserializeObject<double[][][]>(fileContent);
                panel1.NeuralNetwork.SetWeights(weights);
                panel1.BoardState = panel1.GameState;
                panel1.Refresh();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileContent = JsonConvert.SerializeObject(panel1.NeuralNetwork.GetWeights());
                File.WriteAllText(saveFileDialog1.FileName, fileContent);
            }
        }

        private void InitializeComponent()
        {
            this.panel1 = new Basics.AI.NeuralNetworks.Demos.NeuralTicTacToe.TicTacToeNeuralBoardControl();
            this.trainDataSetsControl = new System.Windows.Forms.ComboBox();
            this.trainButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(398, 341);
            this.panel1.TabIndex = 0;
            this.panel1.OnAction += new Basics.Games.Forms.GameActionHandler<Basics.Games.TicTacToe.GameAction>(this.panel1_OnAction);
            // 
            // trainDataSetsControl
            // 
            this.trainDataSetsControl.FormattingEnabled = true;
            this.trainDataSetsControl.Location = new System.Drawing.Point(6, 19);
            this.trainDataSetsControl.Name = "trainDataSetsControl";
            this.trainDataSetsControl.Size = new System.Drawing.Size(193, 21);
            this.trainDataSetsControl.TabIndex = 1;
            // 
            // trainButton
            // 
            this.trainButton.Location = new System.Drawing.Point(30, 46);
            this.trainButton.Name = "trainButton";
            this.trainButton.Size = new System.Drawing.Size(75, 23);
            this.trainButton.TabIndex = 2;
            this.trainButton.Text = "Train";
            this.trainButton.UseVisualStyleBackColor = true;
            this.trainButton.Click += new System.EventHandler(this.trainButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(119, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "txt";
            this.saveFileDialog1.Filter = "Text files (*.txt)|*.txt";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "txt";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Text files (*.txt)|*.txt";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Load";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.trainDataSetsControl);
            this.groupBox1.Controls.Add(this.trainButton);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(205, 88);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Training";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(3, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Neural Network";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(621, 347);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(407, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(211, 341);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // TicTacToeNeuralNetworkDemoForm
            // 
            this.ClientSize = new System.Drawing.Size(621, 347);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TicTacToeNeuralNetworkDemoForm";
            this.Load += new System.EventHandler(this.TicTacToeNeuralNetworkDemoForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
