using Basics.AI.NeuralNetworks.TicTacToe;
using Basics.Games.Demos.TicTacToe;
using Basics.Games.TicTacToe;
using Basics.Main.UI;
using System.Linq;

namespace Basics.AI.NeuralNetworks.Demos.PerceptronTicTacToe
{
    public class PerceptronTicTacToeBoardControl : TicTacToeBoardControl<PerceptronTicTacToeBoardFieldControl>
    {
        public readonly NeuralNetworks.Perceptron Perceptron = new TicTacToePerceptron();
        public GameState GameState { get; private set; }

        public PerceptronTicTacToeBoardControl()
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
                        GameState nextGameState = TicTacToeGame.Instance.Play(value, gameAction);
                        double[] input = new double[9];
                        nextGameState.ToArray(TicTacToeNeuralIOLoader.InputFunctions.Unipolar, input);
                        fieldControl.Output = Perceptron.Sum(input);
                    }

                    fieldControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                }

                if (value.CurrentPlayer.FieldState == FieldState.Cross)
                {
                    this.Fields.Where(f => f.FieldState == FieldState.Empty).OrderByDescending(f => f.Output).First().BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                }
                else
                {
                    this.Fields.Where(f => f.FieldState == FieldState.Empty).OrderBy(f => f.Output).First().BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                }
            }
        }
    }
}
