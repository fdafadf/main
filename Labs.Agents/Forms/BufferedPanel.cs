using System.Windows.Forms;

namespace Labs.Agents
{
    public class BufferedPanel : Panel
    {
        public BufferedPanel()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
        }
    }
}
