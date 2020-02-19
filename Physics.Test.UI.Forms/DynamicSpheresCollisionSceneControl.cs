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
    public partial class DynamicSpheresCollisionSceneControl : UserControl
    {
        ScenePainter formScene;

        public DynamicSpheresCollisionSceneControl()
        {
            InitializeComponent();
            formScene = new ScenePainter(new DynamicCase());
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            formScene.PaintDetails(e);
        }

        PaintableSphere3d selectedSphere = null;
        Pen spherePen = new Pen(Brushes.DarkGray, 2);
        Pen selectedSpherePen = new Pen(Brushes.Black, 2);
        Vector3d mousePosition = new Vector3d();

        protected override void OnKeyDown(KeyEventArgs keys)
        {
            switch (keys.KeyCode)
            {
                case Keys.D1:
                    if (selectedSphere != null)
                    {
                        selectedSphere.Pen = spherePen;
                    }

                    selectedSphere = formScene.Objects[0] as PaintableSphere3d;
                    break;
                case Keys.D2:
                    if (selectedSphere != null)
                    {
                        selectedSphere.Pen = spherePen;
                    }

                    selectedSphere = formScene.Objects[1] as PaintableSphere3d;
                    break;
                case Keys.D3:
                    if (selectedSphere != null)
                    {
                        selectedSphere.Pen = spherePen;
                    }

                    selectedSphere = formScene.Objects[2] as PaintableSphere3d;
                    break;
                case Keys.D0:
                    selectedSphere = null;
                    break;
            }

            if (selectedSphere != null)
            {
                selectedSphere.Pen = selectedSpherePen;
            }

            Refresh();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (selectedSphere != null)
                {
                    mousePosition.X = e.X;
                    mousePosition.Y = e.Y;
                    Vector3d.Sub(mousePosition, selectedSphere.Sphere.Position, ref selectedSphere.Sphere.Direction);
                    Refresh();
                }
            }
        }
    }
}
