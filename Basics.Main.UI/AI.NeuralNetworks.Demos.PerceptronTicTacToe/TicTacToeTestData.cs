using Basics.AI.NeuralNetworks;
using Basics.Games.TicTacToe;
using Basics.Main.UI;
using System.Text;

namespace Basics.AI.NeuralNetworks.Demos.PerceptronTicTacToe
{
    public class TicTacToeNeuralIO : GameStateNeuralIO<GameState>
    {
        public TicTacToeNeuralIO(GameState gameState, double output) : base(gameState, gameState.ToNeuralInput(), output)
        {
        }

        //public NeuralIO ToGenericTestData()
        //{
        //    return new NeuralIO(GameState.ToPerceptronInput(), Output);
        //}

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(GameState.ToString());
            builder.AppendLine($"Current Player: {GameState.CurrentPlayer.FieldState}");
            builder.AppendLine($"Output: {Output}");
            return builder.ToString();
        }
    }
}
