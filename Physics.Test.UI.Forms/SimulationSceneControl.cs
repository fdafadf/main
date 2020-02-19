using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class SimulationSceneControl : UserControl
    {
        ScenePainter formScene;

        public SimulationSceneControl()
        {
            InitializeComponent();
            formScene = new ScenePainter(new SimulationScene(50, 600, 400));
            formScene.Update(0.1f);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            formScene.PaintDetails(e);
        }

        public override void Refresh()
        {
            formScene.Update(1.1f);
            base.Refresh();
        }
    }
}
