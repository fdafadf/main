using System;
using System.Windows.Forms;

namespace Labs.Agents
{
    public static class Extensions
    {
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

        public static void InvokeAction(this Form self, Action action)
        {
            self.Invoke((MethodInvoker)delegate
            {
                action();
            });
        }
    }
}
