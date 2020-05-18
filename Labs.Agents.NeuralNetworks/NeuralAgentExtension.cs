using AI.NeuralNetworks;
using Games.Utilities;
using Labs.Agents.Forms;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Labs.Agents.NeuralNetworks
{
    public class NeuralAgentExtension : LabFormExtension<SimulationNeuralAgentDriverDefinition>
    {
        public NeuralAgentExtension(ILabForm labForm, Workspace workspace) : base(labForm, workspace)
        {
            AddNewAgentMenuItem("&Neural Agent", menuItemNewAgentOnClick);

            //ToolStripMenuItem menuItemNewNetwork = new ToolStripMenuItem();
            //menuItemNewNetwork.Name = "menuItemNewNetwork";
            //menuItemNewNetwork.Size = new System.Drawing.Size(98, 22);
            //menuItemNewNetwork.Text = "&New";
            //menuItemNewNetwork.Click += new System.EventHandler(this.menuItemNewNetwork_Click);
            //
            //ToolStripMenuItem networkToolStripMenuItem = new ToolStripMenuItem();
            //networkToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            //menuItemNewNetwork});
            //networkToolStripMenuItem.Name = "networkToolStripMenuItem";
            //networkToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            //networkToolStripMenuItem.Text = "&Neural Network";
            //LabForm.MenuAgentDrivers.Items.Add(networkToolStripMenuItem);
        }

        private void menuItemNewNetwork_Click(object sender, EventArgs e)
        {
            ShowNewNetworkDialog();
        }

        private void menuItemNewAgentOnClick(object sender, EventArgs e)
        {
            using (var form = new PropertyGridForm("New Agent"))
            {
                var driverDefinition = new SimulationNeuralAgentDriverDefinition(string.Empty, string.Empty, 0);
                form.PropertyGrid.GetToolStrip().AddButton("New Network", ShowNewNetworkDialog);
                form.PropertyGrid.SelectedObject = driverDefinition;
                form.FormClosing += (s, closingEvent) =>
                {
                    if (form.DialogResult == DialogResult.OK)
                    {
                        try
                        {
                            Assert.NotNullOrWhiteSpace(driverDefinition.Name, "Property Name is required.");
                            Assert.NotNullOrWhiteSpace(driverDefinition.Network, "Property Network is required.");
                            Assert.Unique(driverDefinition.Name, Workspace.Instance.AgentsDrivers.Select(driver => driver.Name), "Name already exists.");
                            Workspace.Instance.AgentsDrivers.Add(driverDefinition);
                            Add(driverDefinition);
                        }
                        catch (Exception exception)
                        {
                            closingEvent.Cancel = true;
                            MessageBox.Show(exception.Message, "New Neural Agent", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                };
                form.ShowDialog();
            }
        }

        private void ShowNewNetworkDialog()
        {
            using (var form = new PropertyGridForm("New Network"))
            {
                var networkDefinition = new AgentNetworkDefinition(string.Empty, 2, 26, 12);
                form.PropertyGrid.SelectedObject = networkDefinition;
                form.FormClosing += (s, closingEvent) =>
                {
                    if (form.DialogResult == DialogResult.OK)
                    {
                        try
                        {
                            var network = new AgentNetwork(networkDefinition, new He(0));
                            Workspace.Instance.Add(networkDefinition.Name, network);
                        }
                        catch (Exception exception)
                        {
                            closingEvent.Cancel = true;
                            MessageBox.Show(exception.Message, "New Network", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                };
                form.ShowDialog();
            }
        }

        protected override void Add(SimulationNeuralAgentDriverDefinition driverDefinition)
        {
            var item = new ListViewItem(driverDefinition.Name);
            item.SubItems.Add("Neural");
            item.SubItems.Add(driverDefinition.Description);
            item.Tag = driverDefinition;
            LabForm.AgentDrivers.Items.Add(item);
        }
    }
}
