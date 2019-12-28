using System.Windows.Forms;

namespace Demos.Forms.Base
{
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
