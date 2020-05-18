using System.Collections.Generic;

namespace Games.Go
{
    public class GoGame : IGame<GameState, FieldCoordinates, Stone>
    {
        public IEnumerable<FieldCoordinates> GetAllowedActions(GameState state)
        {
            return state.GetAllowedActions();
        }

        public GameState Play(GameState state, FieldCoordinates action)
        {
            return state.Play(action);
        }
    }

    public class GameAction : IGameAction
    {
        public FieldCoordinates Field;
    }

    public class Player : IPlayer
    { }
}
