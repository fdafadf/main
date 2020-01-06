using Games.Go;
using Games.Sgf;
using Games.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Demos.Forms.Go.Game
{
    /// <summary>
    /// [GoBoardControl]
    ///    -> OnBoardAction
    ///       -> Play
    /// Play
    ///    -> 
    /// [PreparedPositions]
    ///    -> OnPositionSelected
    ///       -> UpdatePathSelectorControl
    ///       -> UpdateNavigationControl
    /// [Navigation]
    ///    -> OnNavigated
    ///       -> Undo (0..*)
    ///       -> Play (0..*)
    /// </summary>
    public class GoBoardController
    {
        GoBoardControl BoardControl;
        //NavigationScrollBar Navigation;
        ComboBox PathSelector;
        ComboBox PreparedPositions;



        public GoBoardController(GoBoardControl boardControl)
        {
            BoardControl = boardControl;
            //currentState = new GameState(boardControl.BoardSize);
        }

        public void RegisterPathSelector(ComboBox pathSelector)
        {
            PathSelector = pathSelector;
            PathSelector.SelectedIndexChanged += PathSelector_SelectedIndexChanged;
        }
        
        protected void SetCurrentPath(FieldCoordinates[] path)
        {

        }

        private void UpdateNavigationControl(FieldCoordinates[] path)
        {
            //if (Navigation != null)
            //{
            //    if (path == null)
            //    {
            //        Navigation.Maximum = 0;
            //        Navigation.Enabled = false;
            //    }
            //    else
            //    {
            //        //while (Mcts.Undo()) { }
            //
            //        Navigation.Tag = path;
            //        Navigation.Value = 0;
            //        Navigation.Maximum = path.Length;
            //        Navigation.Value = path.Length;
            //    }
            //}
        }

        private void SetPathSelectorItem(string name, FieldCoordinates[] path)
        {
            if (PathSelector != null)
            {
                var items = PathSelector.Items.OfType<NamedObject<FieldCoordinates[]>>();
                int itemIndex = items.IndexOf(p => name.Equals(p.Name, StringComparison.OrdinalIgnoreCase));

                if (itemIndex == -1)
                {
                    NamedObject<FieldCoordinates[]> item = new NamedObject<FieldCoordinates[]>(name, path);
                    PathSelector.SelectedIndex = PathSelector.Items.Add(item);
                }
                else
                {
                    NamedObject<FieldCoordinates[]> item = items.ElementAt(itemIndex);
                    item.Value = path;
                    PathSelector.SelectedIndex = -1;
                    PathSelector.SelectedIndex = itemIndex;
                }
            }
        }

        private void PathSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = PathSelector.SelectedItem as NamedObject<FieldCoordinates[]>;
            UpdateNavigationControl(item?.Value);
        }

        private void Navigation_ValueChanged(object sender, GameTreePath<FieldCoordinates> track)
        {
            //navigator.Navigate(track);
            //GameState nextState = currentState.Play(action);
            //
            //if (nextState != null)
            //{
            //    Navigation.Play(action);
            //    currentState = nextState;
            //    UpdateBoardControl();
            //}

            //var selectedPath = Navigation.SelectedPath;
            //var selectedPathLength = Navigation.Value;
            //
            //if (selectedPath != null)
            //{
            //    //int boardDepth = Mcts.CurrentNode.Depth;
            //    //int targetDepth = selectedPath.Value;
            //    
            //    while (selectedPathLength < boardDepth)
            //    {
            //        //Mcts.Undo();
            //        //boardDepth--;
            //    }
            //    
            //    while (selectedPathLength > boardDepth)
            //    {
            //        //Mcts.Move(path.ElementAt(boardDepth));
            //        //boardDepth++;
            //    }
            //    
            //    //RefreshMainBoard();
            //}
        }
    }
}
