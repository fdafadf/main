using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs.Agents.Forms
{
    public partial class SpaceTemplateGeneratorForm : Form
    {
        public SpaceTemplateGeneratorProperties Properties { get; private set; }
        public SpaceTemplate SpaceTemplate { get; private set; }

        public SpaceTemplateGeneratorForm()
        {
            InitializeComponent();
            propertyGrid1.SelectedObject = Properties = new SpaceTemplateGeneratorProperties("");
            Generate();
        }

        private void loadFileButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Settings.SpacesDirectory.FullName;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SpaceTemplate = new SpaceTemplate(new Bitmap(openFileDialog1.FileName));
                pictureBox1.Image = SpaceTemplate.Bitmap;
                Properties.Width = SpaceTemplate.Bitmap.Width;
                Properties.Height = SpaceTemplate.Bitmap.Height;
                Properties.NumberOfAgents = SpaceTemplate.NumberOfAgents;
                propertyGrid1.Refresh();
            }
        }

        private void Generate()
        {
            var obstacles = SpaceTemplateGenerator.GenerateObstacles(Properties);
            var agents = SpaceTemplateGenerator.GenerateAgents(Properties, obstacles);
            SpaceTemplate = new SpaceTemplate(obstacles, agents);
            pictureBox1.Image = SpaceTemplate.Bitmap;
        }

        private void generateAgentsButton_Click(object sender, EventArgs e)
        {
            Properties.Seed = Guid.NewGuid().GetHashCode();
            propertyGrid1.Refresh();
            SpaceTemplateGenerator.GenerateAgents(Properties, SpaceTemplate);
            pictureBox1.Image = SpaceTemplate.Bitmap;
        }

        private void generateButton_ButtonClick(object sender, EventArgs e)
        {
            Properties.Seed = Guid.NewGuid().GetHashCode();
            propertyGrid1.Refresh();
            Generate();
        }
    }
}
