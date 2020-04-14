using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs.Agents
{
    public partial class EnvironmentGeneratorForm : Form
    {
        public EnvironmentGeneratorProperties Properties { get; private set; }
        public EnvironmentGeneratorBitmap EnvironmentBitmap { get; private set; }

        public EnvironmentGeneratorForm()
        {
            InitializeComponent();
            propertyGrid1.SelectedObject = Properties = new EnvironmentGeneratorProperties();
            Generate();
        }

        private void loadFileButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Settings.EnvironmentsDirectory.FullName;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                EnvironmentBitmap = new EnvironmentGeneratorBitmap(new Bitmap(openFileDialog1.FileName));
                pictureBox1.Image = EnvironmentBitmap.Bitmap;
                Properties.Width = EnvironmentBitmap.Bitmap.Width;
                Properties.Height = EnvironmentBitmap.Bitmap.Height;
                Properties.NumberOfAgents = EnvironmentBitmap.NumberOfAgents;
                propertyGrid1.Refresh();
            }
        }

        private void Generate()
        {
            EnvironmentBitmap = new EnvironmentGeneratorBitmap(Properties.Width, Properties.Height);
            EnvironmentBitmap.GenerateObstacles(Properties);
            GenerateAgents();
        }

        private void GenerateAgents()
        {
            EnvironmentBitmap.GenerateAgents(Properties);
            EnvironmentBitmap.UpdateBitmap();
            pictureBox1.Image = EnvironmentBitmap.Bitmap;
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
        }

        private void generateAgentsButton_Click(object sender, EventArgs e)
        {
            Properties.Seed = Guid.NewGuid().GetHashCode();
            propertyGrid1.Refresh();
            GenerateAgents();
        }

        private void generateButton_ButtonClick(object sender, EventArgs e)
        {
            Properties.Seed = Guid.NewGuid().GetHashCode();
            propertyGrid1.Refresh();
            Generate();
        }
    }
}
