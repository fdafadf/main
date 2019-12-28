using Games.Utilities;
using System;

namespace Games.Utilities
{
    public class RandomGamePlayoutGenerator<TGameState, TGameAction, TPlayer> : IGamePlayoutGenerator<TGameState, TPlayer>
        where TGameState : IGameState<TPlayer>
        where TPlayer : IPlayer
        where TGameAction : IGameAction
    {
        private IGame<TGameState, TGameAction, TPlayer> Game;
        private Func<TGameState, TGameAction> RandomActionMethod;
        private Random Random;

        public RandomGamePlayoutGenerator(IGame<TGameState, TGameAction, TPlayer> game, Random random)
        {
            Random = random;
            Game = game;
            RandomActionMethod = GetRandomAllowedAction;
        }

        public TGameState Generate(TGameState state)
        {
            TGameState currentState = state;

            while (currentState.IsFinal == false)
            {
                TGameAction action = RandomActionMethod(currentState);
                currentState = Game.Play(currentState, action);
#if DEBUG
                if (currentState == null)
                {
                    System.Diagnostics.Debugger.Break();
                }
#endif
            }

            return currentState;
        }

        private TGameAction GetRandomAllowedAction(TGameState state)
        {
            return Random.Next(Game.GetAllowedActions(state));
        }
    }
}
