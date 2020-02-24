using AI.MonteCarlo;
using Games.TicTacToe;
using Games.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToeMcts = AI.MonteCarlo.MCTreeSearch<Games.TicTacToe.TicTacToeGame, Games.TicTacToe.GameState, Games.TicTacToe.GameAction, Games.TicTacToe.Player>;
using TicTacToeMctsNode = AI.MonteCarlo.MCTreeSearchNode<Games.TicTacToe.GameState, Games.TicTacToe.GameAction>;

namespace Demos.Forms.TicTacToe.MonteCarlo
{
    public partial class TicTacToeMonteCarloDemoForm : Form
    {
        public TicTacToeMonteCarloDemoForm()
        {
            InitializeComponent();
        }

        GameState gameState;
        MCTreeSearchNavigator<TicTacToeMcts, TicTacToeGame, TicTacToeMctsNode, GameState, GameAction, Player> mctsNavigator;
        //PVNetworkBasedMCTreeSearch<TicTacToeGame, GameState, GameAction, Player> mcts;

        private void button1_Click(object sender, EventArgs e)
        {
            mctsNavigator.Round(100);
            RefreshLabels();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Console.WriteLine($"MCTS Player: {mcts.Player}");
            //Console.WriteLine();

            mctsNavigator.Round(1);
            //var details = mcts.Round(1);
            //
            //foreach (var node in details.Selection)
            //{
            //    Console.WriteLine(node);
            //    Console.WriteLine();
            //}
            //
            //Console.Write(details.Expansion);
            //Console.WriteLine(" Expansion");
            //Console.WriteLine();
            //
            //if (details.Playout != null)
            //{
            //    foreach (var node in details.Playout)
            //    {
            //        Console.WriteLine(node.Item2);
            //        Console.WriteLine();
            //    }
            //}
            //
            //if (details.Playout == null)
            //{
            //    //bool won = mcts.Player.Equals(details.Expansion.Data.GetWinner());
            //    Console.WriteLine($"Won: ?");
            //    Console.WriteLine();
            //}
            //else
            //{
            //    bool won = mcts.Player.Equals(details.Playout.Last().Item2.GetWinner());
            //    Console.WriteLine($"Won: {won}");
            //    Console.WriteLine();
            //}
            //
            RefreshLabels();
        }

        private void TicTacToeMonteCarloDemoForm_Load(object sender, EventArgs e)
        {
            gameState = new GameState();
            var random = new Random(0);
            var expander = new MCTreeSearchExpander<TicTacToeGame, GameState, GameAction, Player>(TicTacToeGame.Instance, random);
            var playoutGenerator = new GamePlayoutRandomGenerator<GameState, Player, GameAction>(TicTacToeGame.Instance, random);
            var mcts = new TicTacToeMcts(expander, playoutGenerator);
            var rootNode = new TicTacToeMctsNode(null, gameState, null);
            mctsNavigator = new MCTreeSearchNavigator<TicTacToeMcts, TicTacToeGame, TicTacToeMctsNode, GameState, GameAction, Player>(mcts, TicTacToeGame.Instance, rootNode);
            //mcts = new PVNetworkBasedMCTreeSearch<TicTacToeGame, GameState, GameAction, Player>(TicTacToeGame.Instance, new GameState(), new PVNetworkMock<GameState, GameAction>(10), new Random());
        }

        private void ticTacToeBoardControl1_OnAction(GameAction gameAction)
        {
            gameState = TicTacToeGame.Instance.Play(gameState, gameAction);
            mctsNavigator.Play(gameAction);
            RefreshLabels();
            ticTacToeBoardControl1.BoardState = gameState;
        }

        private void RefreshLabels()
        {
            foreach (var entry in ticTacToeBoardControl1.Fields)
            {
                entry.Text = string.Empty;
            }

            if (mctsNavigator.CurrentNode.Children != null)
            {
                foreach (var child in mctsNavigator.CurrentNode.Children)
                {
                    ticTacToeBoardControl1[child.Key.X, child.Key.Y].Text = $"Sim: {child.Value.Visits}\r\nWin: {child.Value.Value}";
                }
            }
        }
    }
}
