using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demos.Forms.Base
{
    public partial class DemoForm<TProperties> : Form where TProperties : class
    {
        public TProperties Properties { get; private set; }
        protected Control OutputControl { get; private set; }

        public DemoForm()
        {
            InitializeComponent();
            Properties = InitializeProperties();

            OutputControl = InitializeMainControl();
            OutputControl.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(OutputControl);

            InitializeMenu();

            propertyGrid1.SelectedObject = Properties;
        }

        protected virtual TProperties InitializeProperties()
        {
            return default(TProperties);
        }

        protected virtual Control InitializeMainControl()
        {
            return new Panel();
        }

        protected virtual void InitializeMenu()
        {
            foreach (Control control in propertyGrid1.Controls)
            {
                ToolStrip toolStrip = control as ToolStrip;

                if (toolStrip != null)
                {
                    InitializeMenuItems(toolStrip);
                    break;
                }
            }
        }

        protected virtual void InitializeMenuItems(ToolStrip toolStrip)
        {
        }
    }
}
