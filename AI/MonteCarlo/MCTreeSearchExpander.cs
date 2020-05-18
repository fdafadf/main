using Games;
using Games.Utilities;
using System;

namespace AI.MonteCarlo
{
    public class MCTreeSearchExpander<TGame, TState, TAction, TPlayer> : MCTreeSearchExpanderBase<TGame, MCTreeSearchNode<TState, TAction>, TState, TAction, TPlayer>
        where TGame : IGame<TState, TAction, TPlayer>
        where TState : IGameState<TPlayer>
        where TAction : IGameAction
        where TPlayer : IPlayer
    {
        Random Random;

        public MCTreeSearchExpander(TGame game, Random random) : base(game)
        {
            Random = random;
        }

        protected override MCTreeSearchNode<TState, TAction> CreateNode(MCTreeSearchNode<TState, TAction> parentNode, TState gameState, TAction action)
        {
            return new MCTreeSearchNode<TState, TAction>(parentNode, gameState, action);
        }

        protected override MCTreeSearchNode<TState, TAction> SelectExpanded(MCTreeSearchNode<TState, TAction> node)
        {
            return Random.Next(node.Children).Value;
        }
    }
}
