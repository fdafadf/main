using System;
using System.Windows.Forms;

namespace Labs.Agents
{
    public class ListViewContextMenuItem
    {
        public readonly ToolStripMenuItem MenuItem;
        public Action<ListViewItem> OnClick;
        public Func<ListViewItem, bool> OnClickCondition;

        public ListViewContextMenuItem(ToolStripMenuItem menuItem)
        {
            MenuItem = menuItem;
        }

        public ListViewContextMenuItem(ToolStripMenuItem menuItem, Action<ListViewItem> onClick, Func<ListViewItem, bool> onClickCondition)
        {
            MenuItem = menuItem;
            OnClick = onClick;
            OnClickCondition = onClickCondition;
        }

        public bool IsOnClickEnabled(ListViewItem item)
        {
            return OnClickCondition == null ? true : OnClickCondition(item);
        }
    }
}
