using OpenTK.Platform;

namespace Basics.Physics.Test.UI
{
    static class Program
    {
        static void Main()
        {
            using (IGameWindow window = new MainWindow(800, 600))
            {
                window.Run(60.0);
            }
        }
    }
}
