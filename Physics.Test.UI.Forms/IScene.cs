using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    //public interface IScene
    //{
    //    List<Sphere3d> Spheres { get; }
    //
    //    void Reset();
    //    void Update(float t);
    //    void Paint(PaintEventArgs e);
    //    void PaintDetails(PaintEventArgs e);
    //    void KeyDown(Keys keyCode);
    //    bool MouseMove(MouseEventArgs e);
    //}

    public interface IPaintable
    {
        void Paint(PaintEventArgs e);
        //void PaintDetails(PaintEventArgs e);
    }

    public interface IScene2
    {
        void UpdateInput();
    }

    //public interface I3dScene
    //{
    //
    //}
    //
    //public interface IFormScene
    //{
    //
    //}
}
