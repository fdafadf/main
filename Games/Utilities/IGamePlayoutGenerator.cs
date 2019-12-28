using System;
using System.Collections.Generic;

namespace Games.Utilities
{
    public interface IGamePlayoutGenerator<TGameState, TPlayer, TGameAction>
        where TGameState : IGameState<TPlayer>
        where TPlayer : IPlayer
    {
        TGameState Generate(TGameState state);
        List<Tuple<TGameAction, TGameState>> GeneratePath(TGameState state);
    }
}
