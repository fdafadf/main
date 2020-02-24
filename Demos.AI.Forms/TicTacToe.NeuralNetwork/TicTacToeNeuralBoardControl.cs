using Games.TicTacToe;
using System.Collections.Generic;
using System.Linq;
using AI.NeuralNetworks;
using AI.TicTacToe;
using AI.NeuralNetworks.Games;
using Demos.Forms.TicTacToe.Perceptron;
using AI.NeuralNetworks.TicTacToe;

namespace Demos.Forms.TicTacToe.NeuralNetwork
{
    public class TicTacToeNeuralBoardControl : TicTacToeBoardControl1<PerceptronTicTacToeBoardFieldControl>
    {
        public Network NeuralNetwork { get; set; }
        public GameState GameState { get; private set; }

        public TicTacToeNeuralBoardControl()
        {
            //NeuralNetwork = new NeuralNetwork();
            GameState = new GameState();
            BoardState = GameState;
        }

        public override GameState BoardState
        {
            set
            {
                GameState = value;

                foreach (var fieldControl in this.Fields)
                {
                    if (value[fieldControl.Coordinates.X, fieldControl.Coordinates.Y] == FieldState.Empty)
                    {
                        GameAction gameAction = new GameAction(fieldControl.Coordinates);
                        GameState nextGameState = TicTacToeGame.Instance.Play(value, gameAction);

                        //if (NeuralNetwork.Output.Length == 9)
                        //{
                        //    NeuralNetwork.Evaluate(GameState);
                        //    fieldControl.Output = NeuralNetwork.Output[fieldControl.Coordinates.Y * 3 + fieldControl.Coordinates.X];
                        //}
                        //else if (NeuralNetwork.Output.Length == 1)
                        //{
                        //    NeuralNetwork.Evaluate(nextGameState);
                        //    fieldControl.Output = NeuralNetwork.Output[0];
                        //}
                        //else
                        //{
                        //    fieldControl.Output = 0;
                        //}

                        var prediction = Find(nextGameState);
                        fieldControl.Output2 = string.Format("o {0}, x {1}, d {2}", prediction.Output[0], prediction.Output[1], prediction.Output[2]);
                    }

                    fieldControl.FieldState = value[fieldControl.Coordinates.X, fieldControl.Coordinates.Y];
                    fieldControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                }

                var emptyFields = this.Fields.Where(f => f.FieldState == FieldState.Empty);

                if (emptyFields.Any())
                {
                    emptyFields.OrderByDescending(f => f.Output).First().BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                }
            }
        }

        private static Dictionary<int, LabeledState<GameState, TicTacToeResultProbabilities>> allStatesByHash;
        
        public static LabeledState<GameState, TicTacToeResultProbabilities> Find(GameState gameState)
        {
            if (allStatesByHash == null)
            {
                allStatesByHash = new Dictionary<int, LabeledState<GameState, TicTacToeResultProbabilities>>();
                var uniqueGameStates = TicTacToeNeuralIOGenerator<TicTacToeResultProbabilities>.Instance.GetAllUniqueStates(new TicTacToeResultProbabilitiesEvaluator(TicTacToeResultProbabilitiesNeuralNetwork.DefaultInputFunction));

                foreach (var item in uniqueGameStates)
                {
                    int hashCode = item.State.GetHashCode();
        
                    if (allStatesByHash.ContainsKey(hashCode) == false)
                    {
                        allStatesByHash.Add(hashCode, item);
                    }
                }
            }
        
            allStatesByHash.TryGetValue(gameState.GetHashCode(), out var result);
            return result;
        }
    }
}
