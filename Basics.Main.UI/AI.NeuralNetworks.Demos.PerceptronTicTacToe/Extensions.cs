using Basics.Games.TicTacToe;

namespace Basics.AI.NeuralNetworks.Demos.PerceptronTicTacToe
{
    public static class Extensions
    {
        public static double ToOutput(this Player self)
        {
            if (self == null)
            {
                return 0;
            }
            else
            {
                return self.FieldState == FieldState.Cross ? 2 : -2;
            }
        }
    }
}
