using AI.NeuralNetworks;
using Games.TicTacToe;
using Games.Utilities;
using Basics.MLNet;
using System.Collections.Generic;
using AI.NeuralNetworks.Games;
using Demos.Forms;
using AI.MonteCarlo;
using System;
using TicTacToeAILoader = System.Func<AI.NeuralNetworks.Games.IGameAI<Games.TicTacToe.GameState, Games.TicTacToe.Player, Games.TicTacToe.GameAction>>;
using AI.Keras;
using TicTacToeMCTS = AI.MonteCarlo.MCTreeSearch<Games.TicTacToe.TicTacToeGame, Games.TicTacToe.GameState, Games.TicTacToe.GameAction, Games.TicTacToe.Player>;

namespace Demo
{
    public class TicTacToeAI
    {
        public static IDictionary<string, TicTacToeAILoader> GetEngines()
        {
            Dictionary<string, TicTacToeAILoader> result = new Dictionary<string, TicTacToeAILoader>();
            result.Add("Computer (Keras)", () => new TicTacToeKeras(Settings.TicTacToeKerasModelPath));
            result.Add("Computer (ML.NET)", () => new TicTacToeMLNet(Settings.TicTacToeMLModelPath));
            result.Add("Computer (Monte Carlo)", () => new TicTacToeMonteCarlo());
            return result;
        }
    }

    class TicTacToeMonteCarlo : IGameAI<GameState, Player, GameAction>
    {
        public TicTacToeMonteCarlo()
        {
        }

        public GameAction GenerateMove(GameState gameState)
        {
            TicTacToeMCTS mcts = new TicTacToeMCTS(TicTacToeGame.Instance, Player.Cross, gameState, 0);
            mcts.Round(1000);
            mcts.Play(new GameAction(1, 1));
            mcts.Play(new GameAction(0, 1));
            return mcts.CurrentNode.Children.MaxItem(e => e.Value.Simulations).Key;
        }
    }
}
