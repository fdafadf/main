using Games;
using Games.Utilities;
using System.Collections.Generic;

namespace Games.Utilities
{
    public sealed class GamePlayoutTree<TState, TAction, TPlayer> : GameTree<GamePlayoutNode<TState, TAction>, TState, TAction>
        where TState : IGameState<TPlayer>
        where TAction : IGameAction
        where TPlayer : IPlayer
    {
        public GamePlayoutTree(GamePlayoutNode<TState, TAction> rootNode) : base(rootNode)
        {
        }

        public GamePlayoutNode<TState, TAction> CreatePlayoutNode(GamePlayoutNodeType type, TState gameState, TAction lastAction, GamePlayoutNode<TState, TAction> parentNode)
        {
            return new GamePlayoutNode<TState, TAction>(type, gameState, lastAction, parentNode);
        }

        public GamePlayoutNode<TState, TAction> Expand(GamePlayoutNode<TState, TAction> parentNode, GamePlayoutNode<TState, TAction> childNode)
        {
            if (parentNode.Children == null)
            {
                parentNode.Children = new Dictionary<TAction, GamePlayoutNode<TState, TAction>>();
            }

            if (parentNode.Children.TryGetValue(childNode.LastAction, out var existingNode))
            {
                return existingNode;
            }
            else
            {
                parentNode.Children.Add(childNode.LastAction, childNode);
                return childNode;
            }
        }
    }
}
