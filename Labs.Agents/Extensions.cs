using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Labs.Agents
{
    public static class Extensions
    {
        public static int Count<T>(this T[,] self, Func<T, bool> match)
        {
            int result = 0;

            for (int x = 0; x < self.GetLength(0); x++)
            {
                for (int y = 0; y < self.GetLength(1); y++)
                {
                    if (match(self[x, y]))
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        public static double Distance(this IPoint self, Point p)
        {
            double dx = self.X - p.X;
            double dy = self.Y - p.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public static bool IsOutside<T>(this T[,] self, int x, int y)
        {
            return x < 0 || y < 0 || x >= self.GetLength(0) || y >= self.GetLength(1);
        }
        public static bool IsInside<T>(this T[,] self, int x, int y)
        {
            return x >= 0 && y >= 0 && x < self.GetLength(0) && y < self.GetLength(1);
        }

        public static T NextEnum<T>(this Random self) where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(self.Next(values.Length));
        }

        public static T Next<T>(this Random self, T[] array)
        {
            return array[self.Next(array.Length)];
        }

        public static T[] Initialize<T>(this T[] self, Func<T> factory)
        {
            for (int i = 0; i < self.Length; i++)
            {
                self[i] = factory();
            }

            return self;
        }

        public static T[,] Initialize<T>(this T[,] self, Func<T> factory)
        {
            for (int x = 0; x < self.GetLength(0); x++)
            {
                for (int y = 0; y < self.GetLength(1); y++)
                {
                    self[x, y] = factory();
                }
            }

            return self;
        }

        public static Action CreateInvoker(this Control self, Action action)
        {
            return () => self.InvokeAction(action);
        }

        public static void InvokeAction(this Control self, Action action)
        {
            if (self.InvokeRequired)
            {
                self.Invoke((MethodInvoker)delegate
                {
                    action();
                });
            }
            else
            {
                action();
            }
        }

        public static IEnumerable<T> Subset<T>(this IEnumerable<T> self, int size, Random random)
        {
            var enumerator = self.GetEnumerator();
            double available = self.Count();
            int selected = 0;
            int needed = size;

            while (selected < size)
            {
                enumerator.MoveNext();

                if (random.NextDouble() < needed / available)
                {
                    yield return enumerator.Current;
                    needed--;
                    selected++;
                }

                available--;
            }
        }

        public static ToolStripSeparator AddSeparator(this ToolStripDropDownButton self)
        {
            var separator = new ToolStripSeparator();
            self.DropDownItems.Add(separator);
            return separator;
        }

        public static ToolStripMenuItem AddMenuItem(this ToolStripDropDownButton self, string text, Action action)
        {
            var menuItem = new ToolStripMenuItem();
            menuItem.Text = text;
            menuItem.Click += new EventHandler((sender, e) => action());
            self.DropDownItems.Add(menuItem);
            return menuItem;
        }

        public static ToolStripSeparator AddSeparator(this ToolStrip self)
        {
            var separator = new ToolStripSeparator();
            self.Items.Add(separator);
            return separator;
        }

        public static ToolStripDropDownButton AddDropDownButton(this ToolStrip self, string text)
        {
            var button = new ToolStripDropDownButton()
            {
                Text = text
            };
            self.Items.Add(button);
            return button;
        }

        public static ToolStripButton AddButton(this ToolStrip self, string text, Action action)
        {
            var button = new ToolStripButton();
            button.Text = text;
            button.Click += new EventHandler((sender, e) => action());
            self.Items.Add(button);
            return button;
        }
    }
}
