using Games.Utilities;
using System;
using System.ComponentModel;
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
        public ListView Simulations => listViewSimulationTemplates;
        public ListView Agents => listViewAgents;
        public ListView Spaces => listViewEnvironments;
        public Workspace Workspace;
        protected SpaceTemplateGeneratorForm EnvironmentGeneratorForm = new SpaceTemplateGeneratorForm();

        public LabForm()
        {
            InitializeComponent();
            tabControl1.SelectedIndex = 2;
            listViewEnvironments.AddContextMenuItem("&Remove", RemoveEnvironment);
            listViewAgents.AddContextMenuItem("&Remove", RemoveAgent);
            listViewSimulationTemplates.AddContextMenuItem("&Run", RunSimulation);
            //listViewSimulationDefinitions.AddContextAction("&Edit", EditSimulation);
            listViewSimulationTemplates.AddContextMenuItem("&Remove", RemoveSimulationTemplate);
            listViewSimulationResults.AddContextMenuItem("&Remove", RemoveSimulationResults);
            listViewSimulationTemplates.DoubleClick += listViewSimulationDefinitions_DoubleClick;
            listViewSimulationResults.DoubleClick += menuItemShowSimulationResults_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Workspace.Spaces.ForEach(Add);
            Workspace.SimulationTemplates.ForEach(Add);
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
            if (Workspace.SimulationResults.Remove(GetSimulationResultsDescription(item)))
            {
                listViewSimulationResults.Items.Remove(item);
            }
        }

        private void RemoveSimulationTemplate(ListViewItem item)
        {
            if (item.Tag is SimulationTemplate template)
            {
                if (Workspace.SimulationTemplates.Remove(template.Definition))
                {
                    listViewSimulationTemplates.Items.Remove(item);
                }
            }
        }

        private void RunSimulation(ListViewItem item)
        {
            if (item.Tag is SimulationTemplate template)
            {
                var simulationForm = template.CreateSimulationForm();
                simulationForm.ShowDialog();
                var simulationResults = simulationForm.Simulation.Results;

                if (simulationResults.Series.First().Value.Count > 0)
                {
                    Workspace.SimulationResults.Add(simulationResults);
                    Add(simulationResults, true);
                }
            }
        }

        private void EditSimulation(ListViewItem item)
        {
            if (item.Tag is SimulationTemplateDefinition template)
            {
                using (var gridForm = new PropertyGridForm("Edit Simulation"))
                {
                    gridForm.PropertyGrid.SelectedObject = template;

                    if (gridForm.ShowDialog() == DialogResult.OK)
                    {
                    }
                }
            }
        }

        private void Add(SimulationResultsDescription simulationResults) => Add(simulationResults, simulationResults, false);
        private void Add(SimulationResults simulationResults) => Add(simulationResults.Description, simulationResults, false);
        private void Add(SimulationResults simulationResults, bool focus) => Add(simulationResults.Description, simulationResults, focus);

        private void Add(SimulationResultsDescription simulationResults, object tag, bool focus)
        {
            var item = new ListViewItem(simulationResults.Date.ToString());
            item.SubItems.Add(simulationResults.Agent);
            item.SubItems.Add(simulationResults.Environment);
            item.SubItems.Add(simulationResults.Length.ToString()); 
            item.Tag = tag;
            listViewSimulationResults.AddWithAutoResize(item);

            if (focus)
            {
                listViewSimulationResults.Focus();
                listViewSimulationResults.SelectedItems.Clear();
                item.Selected = true;
                tabControl1.SelectedTab = tabPageSimulationResults;
            }
        }

        private void Add(ISpaceTemplateFactory spaceDefinition)
        {
            if (spaceDefinition is SpaceTemplateGeneratorDefinition generated)
            {
                Add(generated);
            }
            else if (spaceDefinition is SpaceTemplateBitmap map)
            {
                Add(map);
            }
            else
            {
                throw new Exception();
            }
        }

        private void Add(SpaceTemplateBitmap spaceDefinition)
        {
            var item = new ListViewItem(spaceDefinition.Name);
            var template = spaceDefinition.CreateSpaceTemplate();
            item.SubItems.Add(template.Width.ToString());
            item.SubItems.Add(template.Height.ToString());
            item.SubItems.Add(string.Empty);
            item.SubItems.Add(template.NumberOfAgents.ToString());
            item.SubItems.Add(string.Empty);
            item.Tag = spaceDefinition;
            listViewEnvironments.AddWithAutoResize(item);
        }

        private void Add(SpaceTemplateGeneratorDefinition spaceDefinition)
        {
            var item = new ListViewItem(spaceDefinition.Name);
            item.SubItems.Add(spaceDefinition.GeneratorProperties.Width.ToString());
            item.SubItems.Add(spaceDefinition.GeneratorProperties.Height.ToString());
            item.SubItems.Add(spaceDefinition.GeneratorProperties.Seed.ToString());
            item.SubItems.Add(spaceDefinition.GeneratorProperties.NumberOfAgents.ToString());
            item.SubItems.Add(spaceDefinition.GeneratorProperties.NumberOfObstacles.ToString());
            item.Tag = spaceDefinition;
            listViewEnvironments.AddWithAutoResize(item);
        }

        private void Add(SimulationTemplateDefinition simulationTemplateDefinition)
        {
            try
            {
                Add(new SimulationTemplate(simulationTemplateDefinition));
            }
            catch (Exception exception)
            {
                string message = $"Do you want to remove template '{simulationTemplateDefinition.Name}'?\r\n\r\n{exception.Message}";

                if (MessageBox.Show(message, "Simulation Template Loading Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    Workspace.SimulationTemplates.Remove(simulationTemplateDefinition);
                }
            }
        }

        private void Add(SimulationTemplate simulationTemplate)
        {
            var item = new ListViewItem(simulationTemplate.Definition.Space);
            item.SubItems.Add(simulationTemplate.Definition.SimulationPlugin);
            item.SubItems.Add(simulationTemplate.Definition.Model.IterationLimit.ToString());
            item.SubItems.Add(simulationTemplate.ToString());
            item.Tag = simulationTemplate;
            listViewSimulationTemplates.AddWithAutoResize(item);
        }

        private void listViewSimulationDefinitions_DoubleClick(object sender, EventArgs e)
        {
            if (listViewSimulationTemplates.SelectedItems.Count == 1)
            {
                RunSimulation(listViewSimulationTemplates.SelectedItems[0]);
            }
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
                pictureBoxSpacePreview.Image = spaceTemplate.CreatePreviewImage(3);
            }
        }

        private void menuItemNewEnvironment_Click(object sender, EventArgs e)
        {
            if (EnvironmentGeneratorForm.ShowDialog() == DialogResult.OK)
            {
                var spaceDefinition = new SpaceTemplateGeneratorDefinition(EnvironmentGeneratorForm.Properties);
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
            using (var gridForm = new PropertyGridForm("New Simulation"))
            {
                var simulationDefinition = new SimulationTemplateDefinition(Workspace);
                gridForm.PropertyGrid.SelectedObject = simulationDefinition;
                gridForm.OKAction = () =>
                {
                    Workspace.SimulationTemplates.Add(simulationDefinition);
                    Add(simulationDefinition);
                };
                gridForm.ShowDialog();
            }
        }

        private void menuItemShowSimulationResults_Click(object sender, EventArgs e)
        {
            if (listViewSimulationResults.SelectedItems.Count > 0)
            {
                var results = listViewSimulationResults.SelectedItems.OfType<ListViewItem>().Select(GetSimulationResults);
                var form = new ChartForm("Simulation Results", results);
                form.Show();
            }
        }

        private SimulationResults GetSimulationResults(ListViewItem item)
        {
            if (item.Tag is SimulationResultsDescription description)
            {
                return Workspace.SimulationResults.Get(description);
            }
            else
            {
                return item.Tag as SimulationResults;
            }
        }

        private SimulationResultsDescription GetSimulationResultsDescription(ListViewItem item)
        {
            if (item.Tag is SimulationResults simulationResults)
            {
                return simulationResults.Description;
            }
            else
            {
                return item.Tag as SimulationResultsDescription;
            }
        }
    }
}
