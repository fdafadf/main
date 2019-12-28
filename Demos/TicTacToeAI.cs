using AI.NeuralNetworks;
using Games.TicTacToe;
using Basics.MLNet;
using System.Collections.Generic;
using AI.NeuralNetworks.Games;
using Demos.Forms;

namespace Demo
{
    public class TicTacToeAI
    {
        public static IDictionary<string, IGameAI<GameState, Player, GameAction>> GetEngines()
        {
            Dictionary<string, IGameAI<GameState, Player, GameAction>> result = new Dictionary<string, IGameAI<GameState, Player, GameAction>>();
            result.Add("Computer (ML.NET)", new TicTacToeMLNet(Settings.TicTacToeMLModelPath));
            return result;
        }
    }
}
