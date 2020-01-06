using Games;
using Games.Utilities;

namespace Games.Utilities
{
    public class GamePlayoutNavigator<TGameState, TGameAction, TPlayer> : GameTreeNavigator<GamePlayoutTree<TGameState, TGameAction, TPlayer>, TGameState, TGameAction, TPlayer, GamePlayoutNode<TGameState, TGameAction>>
        where TGameState : IGameState<TPlayer>
        where TGameAction : IGameAction
        where TPlayer : IPlayer
    {
        public GamePlayoutNavigator(GamePlayoutTree<TGameState, TGameAction, TPlayer> gameTree) : base(gameTree)
        {
        }

        //public void Navigate(MCTreeSearchRound<FieldCoordinates, GameState> round)
        //{
        //    while (CurrentNode.Parent != null)
        //    {
        //        DoBackward();
        //    }
        //
        //    var path = round.Playout.Select(t => t.Item1);
        //    var currentNode = CurrentNode;
        //
        //    foreach (var action in path)
        //    {
        //        //GameTree.CreatePlayoutNode()
        //        //currentNode
        //    }
        //
        //    //
        //}
    }
}
