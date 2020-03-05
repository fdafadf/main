using AI;
using Basics.MLNet;
using System.Collections.Generic;
using Demos.Forms;
using AI.MonteCarlo;
using TicTacToeAILoader = System.Func<AI.IActionGenerator<Games.TicTacToe.GameState, Games.TicTacToe.GameAction, Games.TicTacToe.Player>>;
using AI.Keras;
using TicTacToeMcts = AI.MonteCarlo.MCTreeSearch<Games.TicTacToe.TicTacToeGame, Games.TicTacToe.GameState, Games.TicTacToe.GameAction, Games.TicTacToe.Player>;

namespace Demo
{
    public class TicTacToeAI
    {
        public static IDictionary<string, TicTacToeAILoader> GetEngines()
        {
            Dictionary<string, TicTacToeAILoader> result = new Dictionary<string, TicTacToeAILoader>();
            result.Add("Computer (Keras)", () => new TicTacToeKerasAI(Settings.TicTacToeKerasModelPath));
            result.Add("Computer (ML.NET)", () => new TicTacToeMLNet(Settings.TicTacToeMLModelPath));
            result.Add("Computer (Monte Carlo)", () => new TicTacToeMonteCarlo());
            return result;
        }
    }
}
