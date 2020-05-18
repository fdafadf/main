using AI.TicTacToe;
using Core.NetFramework;
using System;
using System.Windows.Forms;

namespace Demos.TicTacToe
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            TicTacToeValueLoader.LoadAllUniqueStates(Storage.Instance);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TicTacToeForm());
        }
    }
}
