using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Labs.Agents.Forms
{
    public partial class PropertyGridForm : Form
    {
        public PropertyGrid PropertyGrid => propertyGrid1;

        public PropertyGridForm()
        {
            InitializeComponent();
        }

        public PropertyGridForm(string title) : this()
        {
            Text = title;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                if (PropertyGrid.SelectedObject is IValidatable validatable)
                {
                    try
                    {
                        validatable.Validate();
                    }
                    catch (Exception exception)
                    {
                        e.Cancel = true;
                        MessageBox.Show(exception.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
    }
}
