using Games;
using Games.Utilities;
using System.Collections.Generic;

namespace Games.Utilities
{
    public sealed class GamePlayoutTree<TGameState, TGameAction, TPlayer> : GameTree<TGameState, TGameAction, GamePlayoutNode<TGameState, TGameAction>>
        where TGameState : IGameState<TPlayer>
        where TGameAction : IGameAction
        where TPlayer : IPlayer
    {
        public GamePlayoutTree(GamePlayoutNode<TGameState, TGameAction> rootNode) : base(rootNode)
        {
        }

        public GamePlayoutNode<TGameState, TGameAction> CreatePlayoutNode(GamePlayoutNodeType type, TGameState gameState, TGameAction lastAction, GamePlayoutNode<TGameState, TGameAction> parentNode)
        {
            return new GamePlayoutNode<TGameState, TGameAction>(type, gameState, lastAction, parentNode);
        }

        public GamePlayoutNode<TGameState, TGameAction> Expand(GamePlayoutNode<TGameState, TGameAction> parentNode, GamePlayoutNode<TGameState, TGameAction> childNode)
        {
            if (parentNode.Children == null)
            {
                parentNode.Children = new Dictionary<TGameAction, GamePlayoutNode<TGameState, TGameAction>>();
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
