using Games.TicTacToe;
using Games.Utilities;
using AI;
using System;
using TicTacToeMctsNavigator = AI.MonteCarlo.MCTreeSearchNavigator<AI.MonteCarlo.MCTreeSearch<Games.TicTacToe.TicTacToeGame, Games.TicTacToe.GameState, Games.TicTacToe.GameAction, Games.TicTacToe.Player>, Games.TicTacToe.TicTacToeGame, AI.MonteCarlo.MCTreeSearchNode<Games.TicTacToe.GameState, Games.TicTacToe.GameAction>, Games.TicTacToe.GameState, Games.TicTacToe.GameAction, Games.TicTacToe.Player>;
using AI.MonteCarlo;

namespace Demo
{
    class TicTacToeMonteCarlo : IActionGenerator<GameState, Player, GameAction>
    {
        public TicTacToeMonteCarlo()
        {
        }

        public GameAction GenerateAction(GameState gameState)
        {
            var expander = new MCTreeSearchExpander<TicTacToeGame, GameState, GameAction, Player>(TicTacToeGame.Instance, new Random(0));
            var playoutGenerator = new GamePlayoutRandomGenerator<GameState, Player, GameAction>(TicTacToeGame.Instance, new Random(0));
            var mcts = new MCTreeSearch<TicTacToeGame, GameState, GameAction, Player>(expander, playoutGenerator);
            TicTacToeMctsNavigator mctsNavigator = new TicTacToeMctsNavigator(mcts, TicTacToeGame.Instance, new MCTreeSearchNode<GameState, GameAction>(null, gameState, null));
            mctsNavigator.Round(1000);
            return mctsNavigator.CurrentNode.Children.MaxItem(e => e.Value.Visits).Key;
        }
    }
}
