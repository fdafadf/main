using Games.Utilities;
using System;
using System.Collections.Generic;

namespace Games.Utilities
{
    public class RandomGamePlayoutGenerator<TGameState, TPlayer, TGameAction> : IGamePlayoutGenerator<TGameState, TPlayer, TGameAction>
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

        public List<Tuple<TGameAction, TGameState>> GeneratePath(TGameState state)
        {
            List<Tuple<TGameAction, TGameState>> result = new List<Tuple<TGameAction, TGameState>>();
            TGameState currentState = state;

            while (currentState.IsFinal == false)
            {
                TGameAction action = RandomActionMethod(currentState);
                currentState = Game.Play(currentState, action);
                result.Add(Tuple.Create(action, currentState));
            }

            return result;
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
