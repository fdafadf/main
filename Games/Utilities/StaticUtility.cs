using System.Runtime.CompilerServices;

namespace Games.Utilities
{
    public static class StaticUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Swap<T>(ref T a, ref T b)
        {
            T tmp = a;
            a = b;
            b = tmp;
        }
    }
}
