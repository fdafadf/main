using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs.Agents.NeuralNetworks
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public PropertyGrid PropertyGrid => propertyGrid1;

        private void Form3_Load(object sender, EventArgs e)
        {
            foreach (Control control in propertyGrid1.Controls)
            {
                ToolStrip toolStrip = control as ToolStrip;

                if (toolStrip != null)
                {
                    var button = new ToolStripButton("New Network");
                    button.DisplayStyle = ToolStripItemDisplayStyle.Text;
                    toolStrip.Items.Add(button);
                    //toolStrip.Items.Add(new ToolStripButton("", Properties.Resources.CollapseAll, propertyGridCollapseAllClick));
                    //toolStrip.Items.Add(new ToolStripButton("", Properties.Resources.ExpandAll, propertyGridExpandAllClick));
                }
            }
        }
    }
}
