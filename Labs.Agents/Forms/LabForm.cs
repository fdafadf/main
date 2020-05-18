using Games.Utilities;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Labs.Agents.Forms
{
    public partial class LabForm : Form, ILabForm
    {
        public MenuStrip MenuAgents => menuStrip1;
        public ToolStripMenuItem MenuNewSimulation => menuNewSimulation;
        public ToolStripMenuItem MenuNewAgent => menuItemNewAgent;
        public ListView Simulations => listViewSimulationDefinitions;
        public ListView Agents => listViewAgents;
        public Workspace Workspace;
        protected SpaceTemplateGeneratorForm EnvironmentGeneratorForm = new SpaceTemplateGeneratorForm();

        public LabForm()
        {
            InitializeComponent();
            tabControl1.SelectedIndex = 2;
            listViewEnvironments.AddContextAction("&Remove", RemoveEnvironment);
            listViewAgents.AddContextAction("&Remove", RemoveAgent);
            listViewSimulationDefinitions.AddContextAction("&Run", RunSimulation);
            listViewSimulationDefinitions.AddContextAction("&Remove", RemoveSimulationDefinition);
            listViewSimulationResults.AddContextAction("&Remove", RemoveSimulationResults);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Workspace.Spaces.ForEach(this.Add);
            Workspace.Simulations.ForEach(Add);
            Workspace.SimulationResults.ForEach(Add);
        }

        private void RemoveEnvironment(ListViewItem item)
        {
            if (Workspace.Spaces.Remove(item.Text))
            {
                listViewEnvironments.Items.Remove(item);
            }
        }

        private void RemoveAgent(ListViewItem item)
        {
            if (Workspace.SimulationPlugins.Remove(item.Tag as ISimulationPluginFactory))
            {
                listViewAgents.Items.Remove(item);
            }
        }

        private void RemoveSimulationResults(ListViewItem item)
        {
            if (Workspace.SimulationResults.Remove(item.Tag as SimulationResults))
            {
                listViewSimulationResults.Items.Remove(item);
            }
        }

        private void RemoveSimulationDefinition(ListViewItem item)
        {
            if (Workspace.Simulations.Remove(item.Tag as SimulationFactory))
            {
                listViewSimulationDefinitions.Items.Remove(item);
            }
        }

        private void RunSimulation(ListViewItem item)
        {
            if (item.Tag is SimulationFactory definition)
            {
                var results = definition.CreateSimulationForm().Show();
                Workspace.SimulationResults.Add(results);
                Add(results);
            }
        }

        private void Add(SimulationResults simulationResults)
        {
            var item = new ListViewItem(simulationResults.Date.ToString());
            item.SubItems.Add(simulationResults.Agent);
            item.SubItems.Add(simulationResults.Environment);
            item.SubItems.Add(simulationResults.Length.ToString()); 
            item.Tag = simulationResults;
            listViewSimulationResults.Items.Add(item);
            listViewSimulationResults.Focus();
            item.Selected = true;
            tabControl1.SelectedTab = tabPageSimulationResults;
        }

        private void Add(ISpaceTemplateFactory spaceDefinition)
        {
            if (spaceDefinition is SpaceTemplateGeneratingDefinition generated)
            {
                Add(generated);
            }
            else if (spaceDefinition is SpaceTemplateBitmapDefinition map)
            {
                Add(map);
            }
            else
            {
                throw new Exception();
            }

            listViewEnvironments.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewEnvironments.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void Add(SpaceTemplateBitmapDefinition spaceDefinition)
        {
            var item = new ListViewItem(spaceDefinition.Name);
            var template = spaceDefinition.CreateSpaceTemplate();
            item.SubItems.Add(template.Width.ToString());
            item.SubItems.Add(template.Height.ToString());
            item.SubItems.Add(string.Empty);
            item.SubItems.Add(template.NumberOfAgents.ToString());
            item.SubItems.Add(string.Empty);
            item.Tag = spaceDefinition;
            listViewEnvironments.Items.Add(item);
        }

        private void Add(SpaceTemplateGeneratingDefinition spaceDefinition)
        {
            var item = new ListViewItem(spaceDefinition.Name);
            item.SubItems.Add(spaceDefinition.GeneratorProperties.Width.ToString());
            item.SubItems.Add(spaceDefinition.GeneratorProperties.Height.ToString());
            item.SubItems.Add(spaceDefinition.GeneratorProperties.Seed.ToString());
            item.SubItems.Add(spaceDefinition.GeneratorProperties.NumberOfAgents.ToString());
            item.SubItems.Add(spaceDefinition.GeneratorProperties.NumberOfObstacles.ToString());
            item.Tag = spaceDefinition;
            listViewEnvironments.Items.Add(item);
        }

        private void listViewEnvironments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewEnvironments.SelectedItems.Count == 1)
            {
                var item = listViewEnvironments.SelectedItems[0];
                var definition = item.Tag as ISpaceTemplateFactory;
                var spaceTemplate = definition.CreateSpaceTemplate();
                var oldImage = pictureBoxSpacePreview.Image;
                pictureBoxSpacePreview.Image = null;
                oldImage?.Dispose();
                pictureBoxSpacePreview.Image = Painter.CreateObstaclesBitmap(spaceTemplate.Width, spaceTemplate.Height, 2, (x, y) => spaceTemplate.Obstacles[x, y]);
            }
        }

        private void menuItemNewEnvironment_Click(object sender, EventArgs e)
        {
            if (EnvironmentGeneratorForm.ShowDialog() == DialogResult.OK)
            {
                var spaceDefinition = new SpaceTemplateGeneratingDefinition(EnvironmentGeneratorForm.Properties);
                Workspace.Spaces.Add(spaceDefinition);
                Add(spaceDefinition);
            }
        }

        private void menuItemOpenMapDirectory_Click(object sender, EventArgs e)
        {
            Process.Start(Workspace.Spaces.SpacesDirectory.FullName);
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (sender is ContextMenuStrip menu)
            {
                if (menu.SourceControl is ListView listView)
                {
                    if (listView.SelectedItems.Count == 1)
                    {
                        return;
                    }
                }
            }

            e.Cancel = true;
        }

        private void menuNewSimulation_Click(object sender, EventArgs e)
        {
            var form2 = new PropertyGridForm("New Simulation");
            var simulationDefinition = new SimulationFactory(Workspace);
            form2.PropertyGrid.SelectedObject = simulationDefinition;

            if (form2.ShowDialog() == DialogResult.OK)
            {
                Workspace.Simulations.Add(simulationDefinition);
                Add(simulationDefinition);
            }
        }

        private void Add(SimulationFactory simulationDefinition)
        {
            var item = new ListViewItem(simulationDefinition.Name);
            item.SubItems.Add(simulationDefinition.Space);
            item.SubItems.Add(simulationDefinition.SimulationPlugin);
            item.SubItems.Add(simulationDefinition.Iterations.ToString());
            item.Tag = simulationDefinition;
            listViewSimulationDefinitions.Items.Add(item);
        }

        private void menuItemShowSimulationResults_Click(object sender, EventArgs e)
        {
            if (listViewSimulationResults.SelectedItems.Count > 0)
            {
                var results = listViewSimulationResults.SelectedItems.OfType<ListViewItem>().Select(item => item.Tag as SimulationResults);
                var form = new ChartForm("Simulation Results", results);
                form.Show();
            }
        }
    }
}
