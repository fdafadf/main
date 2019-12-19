using System.Windows.Forms;

namespace Basics.Games.Forms
{
    public delegate void GameActionHandler<TGameAction>(TGameAction action);

    public abstract class BoardControl<TBoardState, TGameAction> : UserControl
    {
        public event GameActionHandler<TGameAction> OnAction;

        public abstract TBoardState BoardState
        {
            set;
        }

        protected void RaiseFieldSelect(TGameAction gameAction)
        {
            this.OnAction?.Invoke(gameAction);
        }
    }
}
