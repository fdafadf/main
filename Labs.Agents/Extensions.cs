using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Labs.Agents
{
    public static class Extensions
    {
        public static T Deserialize<T>(this FileInfo self) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.TypeNameHandling = TypeNameHandling.All;
            serializer.Converters.Add(new StringEnumConverter());

            using (var stream = self.OpenRead())
            {
                var reader = new JsonTextReader(new StreamReader(stream));
                return serializer.Deserialize(reader) as T;
            }
        }

        public static void Serialize(this FileInfo self, object value)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.TypeNameHandling = TypeNameHandling.All;
            serializer.Formatting = Formatting.Indented;
            serializer.Converters.Add(new StringEnumConverter());

            using (var stream = self.OpenWrite())
            {
                var writer = new JsonTextWriter(new StreamWriter(stream));
                serializer.Serialize(writer, value);
                writer.Flush();
            }
        }

        public static FileInfo GetFile(this DirectoryInfo self, string name)
        {
            return new FileInfo(Path.Combine(self.FullName, name));
        }

        public static DirectoryInfo GetDirectory(this DirectoryInfo self, string name)
        {
            return new DirectoryInfo(Path.Combine(self.FullName, name));
        }

        public static bool IsGoalReached<TAgent>(this TAgent self) where TAgent : IGoalAgent, IAnchoredAgent<TAgent>
        {
            if (self.Goal.Position == Point.Empty)
            {
                return false;
            }
            else
            {
                return self.Goal.Position.X == self.Anchor.Field.X && self.Goal.Position.Y == self.Anchor.Field.Y;
            }
        }

        public static void ForEachTrue(this bool[,] self, Action<int, int> action)
        {
            for (int x = 0; x < self.GetLength(0); x++)
            {
                for (int y = 0; y < self.GetLength(1); y++)
                {
                    if (self[x, y])
                    {
                        action(x, y);
                    }
                }
            }
        }

        public static IEnumerable<T> SelectTrue<T>(this bool[,] self, Func<int, int, T> action)
        {
            for (int x = 0; x < self.GetLength(0); x++)
            {
                for (int y = 0; y < self.GetLength(1); y++)
                {
                    if (self[x, y])
                    {
                        yield return action(x, y);
                    }
                }
            }
        }

        public static Point GetUnusedField(this Random self, ISpace space)
        {
            Point point = new Point();
        
            do
            {
                point.X = self.Next(space.Width);
                point.Y = self.Next(space.Height);
            }
            while (space[point.X, point.Y].IsEmpty == false);
        
            return point;
        }

        public static void AddRange<T>(this List<T> self, int count, Func<T> factory)
        {
            self.AddRange(Enumerable.Range(0, count).Select(i => factory()));
        }

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

        public static Point Add(this Point self, IPoint p)
        {
            return new Point(self.X + p.X, self.Y + p.Y);
        }

        public static bool Equals(this Point self, IPoint p)
        {
            return self.X == p.X && self.Y == p.Y;
        }

        public static bool Equals(this IPoint self, Point p)
        {
            return self.X == p.X && self.Y == p.Y;
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
                if (self.Disposing == false && self.IsDisposed == false)
                {
                    self.Invoke((MethodInvoker)delegate
                    {
                        action();
                    });
                }
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

        public static Series AddSeries(this Chart self, string name, IEnumerable<double> values)
        {
            return AddSeriesInternal(self, name, values);
        }

        private static Series AddSeriesInternal(Chart chart, string name, IEnumerable<double> values, Color? color = null)
        {
            string baseName = name;
            int i = 0;

            while (chart.Series.Any(s => s.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)))
            {
                name = $"{++i} {baseName}";
            }

            Series series = chart.Series.Add(name);
            series.ChartType = SeriesChartType.Line;

            if (color.HasValue)
            {
                series.Color = color.Value;
            }

            foreach (double value in values)
            {
                series.Points.Add(value);
            }

            return series;
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

        public static ToolStripComboBox AddDropDownList(this ToolStrip self, Action<string> action, params string[] items)
        {
            var comboBox = new ToolStripComboBox();
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.Items.AddRange(items);
            comboBox.SelectedIndexChanged += new EventHandler((sender, e) => action(comboBox.SelectedItem as string));
            comboBox.SelectedIndex = 0;
            self.Items.Add(comboBox);
            return comboBox;
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

        public static ToolStrip GetToolStrip(this PropertyGrid self)
        {
            return self.Controls.OfType<ToolStrip>().First();
        }

        public static ToolStripButton AddButton(this ToolStrip self, string text, Action action)
        {
            var button = new ToolStripButton();
            button.Text = text;
            button.Click += new EventHandler((sender, e) => action());
            self.Items.Add(button);
            return button;
        }
        public static IEnumerable<FileInfo> EnumerateFiles2(this DirectoryInfo self, string searchPattern)
        {
            if (self.Exists)
            {
                return self.EnumerateFiles(searchPattern);
            }
            else
            {
                return new FileInfo[0];
            }
        }

        public static void AddWithAutoResize(this ListView self, ListViewItem item)
        {
            self.Items.Add(item);
            self.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            self.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        public static void EnsureContextMenuStrip(this ListView self)
        {
            if (self.ContextMenuStrip == null)
            {
                self.ContextMenuStrip = new ContextMenuStrip();
                self.ContextMenuStrip.Opening += new CancelEventHandler(OnContextActionOpening);
            }
        }

        public static ListViewContextMenuItem AddContextMenuItem(this ListView self, string name, Action<ListViewItem> onClick)
        {
            return self.AddContextMenuItem(name, onClick, null);
        }

        public static ListViewContextMenuItem AddContextMenuItem(this ListView self, string name, Action<ListViewItem> onClick, Func<ListViewItem, bool> onClickCondition)
        {
            self.EnsureContextMenuStrip();
            var menuItem = new ToolStripMenuItem(name);
            var menuItemTag = new ListViewContextMenuItem(menuItem, onClick, onClickCondition);
            menuItem.Click += new EventHandler(OnContextActionSelected);
            menuItem.Tag = menuItemTag;
            self.ContextMenuStrip.Items.Add(menuItem);
            return menuItemTag;
        }

        private static void OnContextActionSelected(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem menuItem)
            {
                if (menuItem.Owner is ContextMenuStrip menu)
                {
                    if (menu.Tag is ListViewItem viewItem)
                    {
                        if (menuItem.Tag is Action<ListViewItem> action)
                        {
                            action(viewItem);
                        }
                        else if (menuItem.Tag is ListViewContextMenuItem tag)
                        {
                            tag.OnClick(viewItem);
                        }
                    }
                }
            }
        }

        private static void OnContextActionOpening(object sender, CancelEventArgs e)
        {
            if (sender is ContextMenuStrip menu)
            {
                if (menu.SourceControl is ListView listView)
                {
                    if (listView.SelectedItems.Count == 1)
                    {
                        var item = listView.SelectedItems[0];

                        foreach (ToolStripMenuItem menuItem in menu.Items)
                        {
                            if (menuItem.Tag is ListViewContextMenuItem tag)
                            {
                                menuItem.Visible = tag.IsOnClickEnabled(item);
                            }
                        }

                        menu.Tag = listView.SelectedItems[0];
                        return;
                    }
                }
            }

            e.Cancel = true;
        }

        public static DirectoryInfo EnsureDirectory(this DirectoryInfo self, string directoryName)
        {
            return self.GetDirectory(directoryName).Ensure();
        }

        public static DirectoryInfo Ensure(this DirectoryInfo self)
        {
            if (self.Exists == false)
            {
                self.Create();
            }

            return self;
        }

        public static FileInfo Ensure(this FileInfo self)
        {
            if (self.Exists == false)
            {
                self.Create().Dispose();
            }

            return self;
        }
    }
}
