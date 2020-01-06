using System;
using System.Collections.Generic;

namespace Games.Utilities
{
    public class GamePlayoutRandomGenerator<TGameState, TPlayer, TGameAction> : IGamePlayoutGenerator<TGameState, TPlayer, TGameAction>
        where TGameState : IGameState<TPlayer>
        where TPlayer : IPlayer
        where TGameAction : IGameAction
    {
        private IGame<TGameState, TGameAction, TPlayer> Game;
        private Func<TGameState, TGameAction> RandomActionProvider;

        public GamePlayoutRandomGenerator(IGame<TGameState, TGameAction, TPlayer> game, Random random)
        {
            Game = game;
            RandomActionProvider = gameState => GetRandomAllowedAction(random, gameState);
        }

        public GamePlayoutRandomGenerator(IGame<TGameState, TGameAction, TPlayer> game, Func<TGameState, TGameAction> randomActionProvider)
        {
            Game = game;
            RandomActionProvider = randomActionProvider;
        }

        public List<Tuple<TGameAction, TGameState>> GeneratePath(TGameState state)
        {
            List<Tuple<TGameAction, TGameState>> result = new List<Tuple<TGameAction, TGameState>>();
            TGameState currentState = state;

            while (currentState.IsFinal == false)
            {
                TGameAction action = RandomActionProvider(currentState);
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
                TGameAction action = RandomActionProvider(currentState);
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

        private TGameAction GetRandomAllowedAction(Random random, TGameState state)
        {
            return random.Next(Game.GetAllowedActions(state));
        }
    }
}
