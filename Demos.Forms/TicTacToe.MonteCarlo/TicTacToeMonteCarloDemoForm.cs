using AI.MonteCarlo;
using Games.TicTacToe;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demos.Forms.TicTacToe.MonteCarlo
{
    public partial class TicTacToeMonteCarloDemoForm : Form
    {
        public TicTacToeMonteCarloDemoForm()
        {
            InitializeComponent();
        }

        GameState gameState;
        MCTreeSearch<TicTacToeGame, GameState, GameAction, Player> mcts;

        private void button1_Click(object sender, EventArgs e)
        {
            mcts.Round(100);
            RefreshLabels();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"MCTS Player: {mcts.Player}");
            Console.WriteLine();

            var details = mcts.RoundWithDetails();

            foreach (var node in details.Selection)
            {
                Console.WriteLine(node);
                Console.WriteLine();
            }

            Console.Write(details.Expansion);
            Console.WriteLine(" Expansion");
            Console.WriteLine();

            if (details.Playout != null)
            {
                foreach (GameState state in details.Playout.Select(n => n.Item2))
                {
                    Console.WriteLine(state);
                    Console.WriteLine();
                }
            }

            if (details.Playout == null)
            {
                //bool won = mcts.Player.Equals(details.Expansion.Data.GetWinner());
                Console.WriteLine($"Won: ?");
                Console.WriteLine();
            }
            else
            {
                bool won = mcts.Player.Equals(details.Playout.Last().Item2.GetWinner());
                Console.WriteLine($"Won: {won}");
                Console.WriteLine();
            }

            RefreshLabels();
        }

        private void TicTacToeMonteCarloDemoForm_Load(object sender, EventArgs e)
        {
            gameState = new GameState();
            mcts = new MCTreeSearch<TicTacToeGame, GameState, GameAction, Player>(TicTacToeGame.Instance, Player.Cross, gameState, 0);
        }

        private void ticTacToeBoardControl1_OnAction(GameAction gameAction)
        {
            gameState = TicTacToeGame.Instance.Play(gameState, gameAction);
            mcts.Move(gameAction);
            RefreshLabels();
            ticTacToeBoardControl1.BoardState = gameState;
        }

        private void RefreshLabels()
        {
            foreach (var entry in ticTacToeBoardControl1.Fields)
            {
                entry.Text = string.Empty;
            }

            if (mcts.CurrentNode.Children != null)
            {
                foreach (var child in mcts.CurrentNode.Children)
                {
                    ticTacToeBoardControl1[child.Key.X, child.Key.Y].Text = $"Sim: {child.Value.Simulations}\r\nWin: {child.Value.Wins}";
                }
            }
        }
    }
}
