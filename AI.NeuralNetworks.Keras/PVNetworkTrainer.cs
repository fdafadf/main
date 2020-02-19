using AI.MonteCarlo;
using Games;
using Games.TicTacToe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToeMCTreeSearchPVNetwork = AI.MonteCarlo.PVNetworkBasedMCTreeSearch<Games.TicTacToe.TicTacToeGame, Games.TicTacToe.GameState, Games.TicTacToe.GameAction, Games.TicTacToe.Player>;

namespace AI.Keras
{
    class PVNetworkTrainer<TGame, TState, TAction, TPlayer> 
        where TGame : IGame<TState, TAction, TPlayer>
        where TState : IGameState<TPlayer>
        where TAction : IGameAction
        where TPlayer : IPlayer
    {
        public int NumberOfGames = 100;
        public int NumberOfPlayoutsPerMove = 100;

        TState RootState;
        KerasPVNetwork<TState, TAction, PVNetworkOutput<TAction>> Network;
        PVNetworkBasedMCTreeSearchExpander<TGame, TState, TAction, TPlayer> Expander;

        public PVNetworkTrainer(TGame game, TState rootState, KerasPVNetwork<TState, TAction, PVNetworkOutput<TAction>> network)
        {
            RootState = rootState;
            Network = network;
            Expander = new PVNetworkBasedMCTreeSearchExpander<TGame, TState, TAction, TPlayer>(game, new Random(0), Network);
        }

        public void Epoch()
        {
            var finalNodes = Selfplay().ToList();
            Console.Title = "Training";
            Network.Train(finalNodes, 20);
        }

        private IEnumerable<PVNetworkBasedMCTreeSearchNode<TState, TAction>> Selfplay()
        {
            List<PVNetworkBasedMCTreeSearchNode<TState, TAction>> result = new List<PVNetworkBasedMCTreeSearchNode<TState, TAction>>();

            //Parallel.ForEach(Enumerable.Range(0, NumberOfGames), number =>
            //{
            //    var mcts = new PVNetworkBasedMCTreeSearch<TGame, TState, TAction, TPlayer>(Game, RootState, Network, Random);
            //
            //    while (mcts.CurrentNode.GameState.IsFinal == false)
            //    {
            //        mcts.Round(NumberOfPlayoutsPerMove);
            //        var bestChild = mcts.CurrentNode.GetMostVisitedChild();
            //        mcts.Play(bestChild.LastAction);
            //    }
            //
            //    result.Add(mcts.CurrentNode);
            //});



            for (int gameNumber = 0; gameNumber < NumberOfGames; gameNumber++)
            {
                Console.Title = $"Selfplay {gameNumber + 1}/{NumberOfGames}";
                var mcts = new PVNetworkBasedMCTreeSearch<TGame, TState, TAction, TPlayer>(Expander);
                //var actions = Expander.Game.GetAllowedActions(RootState);
                var networkOutput = Network.Predict(RootState);
                var node = PVNetworkBasedMCTreeSearchNode<TState, TAction>.CreateRoot(RootState, networkOutput);

                while (node.State.IsFinal == false)
                {
                    for (int i = 0; i < NumberOfPlayoutsPerMove; i++)
                    {
                        MCTreeSearchRound<PVNetworkBasedMCTreeSearchNode<TState, TAction>, TState, TAction> round = mcts.RoundWithDetails(node);
                        var ticTacToeRound = round as MCTreeSearchRound<PVNetworkBasedMCTreeSearchNode<GameState, GameAction>, GameState, GameAction>;

                        if (ticTacToeRound != null)
                        {
                            ticTacToeRound.WriteTo(Console.Out);
                            Console.ReadLine();
                        }
                    }

                    //ConsoleUtility.WriteLine(node);
                    node = node.GetMostVisitedChild();
                }
                
                result.Add(node);
            }

            return result;
        }
    }
}
