using System.Collections.Generic;

namespace Games.Utilities
{
    public class GameTreePath<TAction>
    {
        public static IEnumerable<TAction> Empty = new TAction[0];

        public IEnumerable<TAction> Backward;
        public IEnumerable<TAction> Forward;

        public GameTreePath(IEnumerable<TAction> backward, IEnumerable<TAction> forward)
        {
            Backward = backward;
            Forward = forward;
        }
    }
}
