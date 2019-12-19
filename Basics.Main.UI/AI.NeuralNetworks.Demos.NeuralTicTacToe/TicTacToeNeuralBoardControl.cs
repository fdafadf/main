using Basics.AI.NeuralNetworks;
using Basics.AI.NeuralNetworks.Demos.PerceptronTicTacToe;
using Basics.Games.Demos.TicTacToe;
using Basics.Games.TicTacToe;
using Basics.Main.UI;
using System.Linq;

namespace Basics.AI.NeuralNetworks.Demos.NeuralTicTacToe
{
    public class TicTacToeNeuralBoardControl : TicTacToeBoardControl<PerceptronTicTacToeBoardFieldControl>
    {
        public readonly NeuralNetwork NeuralNetwork = TicTacToeNeuralNetwork.Create();
        public readonly TicTacToeGame Game = new TicTacToeGame();
        public GameState GameState { get; private set; }

        public TicTacToeNeuralBoardControl()
        {
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
                    fieldControl.FieldState = value[fieldControl.Coordinates.X, fieldControl.Coordinates.Y];

                    if (fieldControl.FieldState == FieldState.Empty)
                    {
                        GameAction gameAction = fieldControl.Coordinates.ToGameAction();
                        GameState nextGameState = Game.Play(value, gameAction);
                        double[] input = new double[9];
                        GameState.ToNeuralInput(input);
                        double[] output = NeuralNetwork.Evaluate(input);
                        fieldControl.Output = output[fieldControl.Coordinates.Y * 3 + fieldControl.Coordinates.X];
                    }

                    fieldControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                }

                if (value.CurrentPlayer.FieldState == FieldState.Cross)
                {
                    //this.Fields.Where(f => f.FieldState == FieldState.Empty).OrderByDescending(f => f.Output).First().BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                }
                else
                {
                    //this.Fields.Where(f => f.FieldState == FieldState.Empty).OrderBy(f => f.Output).First().BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                }
            }
        }
    }
}
