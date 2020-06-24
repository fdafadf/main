using System;

namespace Labs.AI
{
    class Assert
    {
        public static void Equals(int a, int b)
        {
            if (a != b)
            {
                throw new ArgumentException();
            }
        }
    }
}
