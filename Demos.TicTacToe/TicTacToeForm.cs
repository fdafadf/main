using AI.NeuralNetworks.Games;
using Games.TicTacToe;
using SimpleNeuralNetwork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class TicTacToeForm : Form
    {
        public TicTacToeForm()
        {
            InitializeComponent();
        }

        #region Event Handlers

        private void buttonCreateAndTrain_Click(object sender, EventArgs e)
        {
            StartTraining();
        }

        private void workerTrain_DoWorkAsync(object sender, DoWorkEventArgs e)
        {
            Training(e.Argument as TrainingSettings);
        }

        private void buttonTrained_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }

        private void buttonCircle_Click(object sender, EventArgs e)
        {
            circleAI = null;
            crossAI = new TicTacToeAI(TrainedNetwork);
            Play();
        }

        private void buttonCross_Click(object sender, EventArgs e)
        {
            circleAI = new TicTacToeAI(TrainedNetwork);
            crossAI = null;
            Play();
        }

        #endregion
        #region Playing

        GameState currentState;
        IGameAI<GameState, Player, GameAction> circleAI;
        IGameAI<GameState, Player, GameAction> crossAI;

        private void Play()
        {
            currentState = new GameState();
            boardControl.BoardState = currentState;
            tabControl1.SelectedIndex = 3;
            TryToPlayAsAI();
        }

        private IGameAI<GameState, Player, GameAction> CurrentAI
        {
            get
            {
                if (currentState.CurrentPlayer.FieldState == FieldState.Cross)
                {
                    return crossAI;
                }
                else
                {
                    return circleAI;
                }
            }
        }

        private void boardControl_OnAction(GameAction action)
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
                        boardControl.BoardState = nextState;
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

                tabControl1.SelectedIndex = 2;
            }
            else
            {
                TryToPlayAsAI();
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
                        boardControl.BoardState = nextState;
                        currentState = nextState; MovePlayed();
                    }
                }
            }
        }

        #endregion
        #region Training

        Network TrainedNetwork;

        private void StartTraining()
        {
            TrainingSettings settings = new TrainingSettings
            {
                Layers = textBoxLayers.Text.Split(',').Select(v => int.Parse(v)).ToArray(),
                Epoches = (int)numericEpoches.Value,
                LearningRate = (double)numericLearningRate.Value,
                Momentum = (double)numericMomentum.Value
            };

            progressBarTraining.Maximum = settings.Epoches;
            labelMeanSquareError.Text = $"{double.PositiveInfinity}";
            labelAccuracy.Text = string.Empty;
            labelEpoch.Text = string.Empty;
            buttonTrained.Enabled = false;
            tabControl1.SelectedIndex = 1;
            workerTrain.RunWorkerAsync(settings);
        }

        private void Training(TrainingSettings settings)
        {
            int epoches = settings.Epoches;
            var network = new Network(Function.ReLU, 9, 3, settings.Layers);
            var optimizer = new SGDMomentum(network, settings.LearningRate, settings.Momentum);
            var trainer = new Trainer(optimizer);
            trainer.Monitors.Add(new TrainingMonitor(this));
            trainer.Train(DataLoader.TrainingFeatures, DataLoader.TrainingLabels, epoches);
            TrainedNetwork = network;

            Invoke((MethodInvoker)delegate ()
            {
                var accuracy = AccuracyMonitor.CalculateAccuracy(optimizer);
                labelAccuracy.Text = $"{accuracy.CorrectPredictions}/{accuracy.TestingSetSize} ({accuracy.Value * 100.0:f2}%)";
                buttonTrained.Enabled = true;
            });
        }

        class TrainingMonitor : MeanSquareErrorMonitor
        {
            TicTacToeForm Form;
            int epoch;

            public TrainingMonitor(TicTacToeForm form)
            {
                Form = form;
            }

            protected override void OnEpoch(double meanSquareError)
            {
                Form.Invoke((MethodInvoker)delegate ()
                {
                    epoch++;
                    Form.labelMeanSquareError.Text = $"{meanSquareError:F6}";
                    Form.progressBarTraining.Value = epoch;
                    Form.labelEpoch.Text = $"{epoch}/{Form.progressBarTraining.Maximum}";
                });
            }
        }

        #endregion
    }
}
