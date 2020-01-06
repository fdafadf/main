using Games.Go;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Demos.Forms.Go.Game
{
    public class GoBoardControlFieldFeatures
    {
        public class FieldLabel
        {
            public string Text;

            StringFormat format = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            public void Paint(Graphics graphics, GoBoardControlGeometry geometry, int fx, int fy)
            {
                if (Text != null)
                {
                    RectangleF rect = new RectangleF(fx, fy, geometry.FieldWidth, geometry.FieldWidth);
                    graphics.DrawString(Text, SystemFonts.DefaultFont, Brushes.Gray, rect, format);
                }
            }
        }

        public class FieldLabelCollection
        {

        }

        public FieldState State;
        public bool[] Borders = new bool[4];
        public FieldLabel Label = new FieldLabel();
        public List<KeyValuePair<Brush, string>> Labels = new List<KeyValuePair<Brush, string>>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddLabel(Brush brush, string value)
        {
            Labels.Add(new KeyValuePair<Brush, string>(brush, value));
        }
    }
}
