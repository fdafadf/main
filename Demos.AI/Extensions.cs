using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Demo
{
    static class Extensions
    {
        public static readonly Random Random = new Random();

        public static string Concatenate<T>(this IEnumerable<T> self, string separator)
        {
            return string.Join(separator, self);
        }

        public static void SetItems(this ComboBox self, IEnumerable<FileInfo> files)
        {
            self.Items.Clear();

            foreach (FileInfo trainDataFile in files)
            {
                self.Items.Add(trainDataFile);
            }

            if (self.Items.Count > 0)
            {
                self.SelectedIndex = 0;
            }
        }

        public static void ForEach<T>(this T[] self, Action<T> action)
        {
            foreach (T item in self)
            {
                action(item);
            }
        }

        public static string AsString(this double[] self, string format)
        {
            return string.Join(",", self.Select(i => i.ToString(format)));
        }

        public static string AsString<T>(this T[] self)
        {
            return string.Join(",", self.Select(i => i.ToString()));
        }
    }
}
