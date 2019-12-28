using AI.NeuralNetworks;
using Games.TicTacToe;
using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using AI.NeuralNetworks.Games;

namespace Demos.Forms.TicTacToe.Game
{
    public partial class TicTacToeGameForm : Form
    {
        GameState currentState = new GameState();
        readonly IGameAI<GameState, Player, GameAction> NoughtAI;
        readonly IGameAI<GameState, Player, GameAction> CrossAI;

        public TicTacToeGameForm(IGameAI<GameState, Player, GameAction> noughtAI, IGameAI<GameState, Player, GameAction> crossAI) : this()
        {
            NoughtAI = noughtAI;
            CrossAI = crossAI;
        }

        public TicTacToeGameForm()
        {
            InitializeComponent();
        }

        private void ticTacToeBoardControl1_OnAction(GameAction action)
        {
            if (CurrentAI == null)
            {
                if (currentState.IsFinal)
                {
                    MessageBox.Show("Game is already finished");
                }
                else
                {
                    GameState nextState = TicTacToeGame.Instance.Play(currentState, action);

                    if (nextState == null)
                    {
                        MessageBox.Show("It action is not allowed");
                    }
                    else
                    {
                        this.boardControl.BoardState = nextState;
                        currentState = nextState;
                        MovePlayed();
                    }
                }
            }
            else
            {
                MessageBox.Show("It is not your turn.");
            }
        }

        private void TicTacToeForm_Load(object sender, System.EventArgs e)
        {
            TryToPlayAsAI();
        }

        private void MovePlayed()
        {
            if (currentState.IsFinal)
            {
                Player winner = currentState.GetWinner();

                if (winner == null)
                {
                    MessageBox.Show($"Game has been finished with draw");
                }
                else
                {
                    MessageBox.Show($"Game winner is {winner.FieldState}");
                }
            }
            else
            {
                TryToPlayAsAI();
            }
        }

        private IGameAI<GameState, Player, GameAction> CurrentAI
        {
            get
            {
                if (currentState.CurrentPlayer.FieldState == FieldState.Cross)
                {
                    return CrossAI;
                }
                else
                {
                    return NoughtAI;
                }
            }
        }

        private void TryToPlayAsAI()
        {
            IGameAI<GameState, Player, GameAction> currentAI = CurrentAI;

            if (currentAI != null)
            {
                if (currentState.IsFinal == false)
                {
                    GameAction gameAction = currentAI.GenerateMove(currentState);
                    GameState nextState = TicTacToeGame.Instance.Play(currentState, gameAction);

                    if (nextState == null)
                    {
                        MessageBox.Show($"The {currentState.CurrentPlayer.FieldState} lost by illegal move.");
                    }
                    else
                    {
                        this.boardControl.BoardState = nextState;
                        currentState = nextState;
                        MovePlayed();
                    }
                }
            }
        }
    }
}
