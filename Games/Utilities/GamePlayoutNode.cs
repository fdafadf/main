using Games.Utilities;

namespace Games.Utilities
{
    public class GamePlayoutNode<TGameState, TGameAction> : GameTreeNode<TGameState, TGameAction, GamePlayoutNode<TGameState, TGameAction>>
    {
        public GamePlayoutNodeType Type;

        public GamePlayoutNode(TGameState gameState) : base(gameState, default(TGameAction), null)
        {
        }

        public GamePlayoutNode(GamePlayoutNodeType type, TGameState gameState, TGameAction lastAction, GamePlayoutNode<TGameState, TGameAction> parent) : base(gameState, lastAction, parent)
        {
            Type = type;
        }
    }
}
