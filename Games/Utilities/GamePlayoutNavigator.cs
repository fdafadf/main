namespace Games.Utilities
{
    public class GamePlayoutNavigator<TState, TAction, TPlayer> : IGameTreeNavigator<GamePlayoutNode<TState, TAction>, TState, TAction>
        where TState : IGameState<TPlayer>
        where TAction : IGameAction
        where TPlayer : IPlayer
    {
        public GamePlayoutNode<TState, TAction> CurrentNode { get; private set; }

        public GamePlayoutTree<TState, TAction, TPlayer> GameTree;

        public GamePlayoutNavigator(GamePlayoutTree<TState, TAction, TPlayer> gameTree)
        {
            GameTree = gameTree;
            CurrentNode = gameTree.Root;
        }

        public bool Backward()
        {
            if (CurrentNode.Parent == null)
            {
                return false;
            }
            else
            {
                CurrentNode = CurrentNode.Parent;
                return true;
            }
        }

        public bool Forward(GamePlayoutNode<TState, TAction> childNode)
        {
            CurrentNode = childNode;
            return true;
        }
    }
    //public class GamePlayoutNavigator<TGameState, TGameAction, TPlayer> : ObservableGameTreeNavigator<IGameTreeNavigator<TGameState, TGameAction, GamePlayoutNode<TGameState, TGameAction>>, TGameState, TGameAction, TPlayer, GamePlayoutNode<TGameState, TGameAction>>
    //    where TGameState : IGameState<TPlayer>
    //    where TGameAction : IGameAction
    //    where TPlayer : IPlayer
    //{
    //    public GamePlayoutNavigator(IGameTreeNavigator<TGameState, TGameAction, GamePlayoutNode<TGameState, TGameAction>> navigator) : base(navigator)
    //    {
    //    }
    //
    //    //public void Navigate(MCTreeSearchRound<FieldCoordinates, GameState> round)
    //    //{
    //    //    while (CurrentNode.Parent != null)
    //    //    {
    //    //        DoBackward();
    //    //    }
    //    //
    //    //    var path = round.Playout.Select(t => t.Item1);
    //    //    var currentNode = CurrentNode;
    //    //
    //    //    foreach (var action in path)
    //    //    {
    //    //        //GameTree.CreatePlayoutNode()
    //    //        //currentNode
    //    //    }
    //    //
    //    //    //
    //    //}
    //}
}
