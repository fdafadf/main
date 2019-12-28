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
            mcts.Rounds(100);
            RefreshLabels();
        }

        private void TicTacToeMonteCarloDemoForm_Load(object sender, EventArgs e)
        {
            gameState = new GameState();
            mcts = new MCTreeSearch<TicTacToeGame, GameState, GameAction, Player>(TicTacToeGame.Instance, gameState, 0);
        }

        private void ticTacToeBoardControl1_OnAction(GameAction gameAction)
        {
            gameState = TicTacToeGame.Instance.Play(gameState, gameAction);
            mcts.Move(gameAction);
            ticTacToeBoardControl1.BoardState = gameState;
        }

        private void RefreshLabels()
        {
            foreach (var entry in ticTacToeBoardControl1.Fields)
            {
                entry.Text = string.Empty;
            }

            foreach (var child in mcts.CurrentNode.Children)
            {
                ticTacToeBoardControl1[child.Key.X, child.Key.Y].Text = $"Sim: {child.Value.Simulations}\r\nWin: {child.Value.Wins}";
            }
        }
    }
}
