using AI.MonteCarlo;
using Games.Go;
using Games.Utilities;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GoMcts = AI.MonteCarlo.MCTreeSearch<Games.Go.GoGame, Games.Go.GameState, Games.Go.FieldCoordinates, Games.Go.Stone>;

namespace Demos.Forms.Go.Game
{
    public class GoBoardControlFieldFeaturesHelper
    {
        public static void RefreshBoard(GoBoardControl boardControl, IObservableGameTreeNavigator<MCTreeSearchNode<GameState, FieldCoordinates>, GameState, FieldCoordinates> navigator)
        {
            GameState currentState = navigator.CurrentNode.State;
            var allowedActions = currentState.GetAllowedActions();

            for (uint y = 0; y < boardControl.BoardSize; y++)
            {
                for (uint x = 0; x < boardControl.BoardSize; x++)
                {
                    FieldCoordinates field = FieldCoordinates.Get(x, y);
                    FieldState fieldState = currentState.InternalState.BoardFields[x, y];
                    boardControl.Fields[x, y].State = fieldState;
                    boardControl.Fields[x, y].Labels.Clear();
                    boardControl.Fields[x, y].Borders[0] = fieldState == FieldState.Empty ? allowedActions.Contains(field) == false : false;
                    boardControl.Fields[x, y].Borders[1] = false;
                    boardControl.Fields[x, y].Borders[2] = false;
                }
            }

            var currentNode = navigator.CurrentNode;
            //double[] mctsWeights = null;// mcts.Selector.GetWeights(mcts.CurrentNode);

            if (currentNode.Children != null)
            {
                foreach (var childNode in currentNode.Children)
                {
                    if (childNode.Key != FieldCoordinates.Pass)
                    {
                        if (childNode.Value.Visits > 0)
                        {
                            boardControl.Fields[childNode.Key.X, childNode.Key.Y].AddLabel(Brushes.Magenta, $"{childNode.Value.Visits / 10}");
                            boardControl.Fields[childNode.Key.X, childNode.Key.Y].AddLabel(Brushes.Green, $"{childNode.Value.Value / 10}");
                        }
                    }
                }

                var mostFrequentlySimulated = currentNode.Children.Where(w => w.Key != FieldCoordinates.Pass && w.Value.Visits > 0).MaxItems(i => i.Value.Visits);

                foreach (var mostItem in mostFrequentlySimulated)
                {
                    boardControl.Fields[mostItem.Key.X, mostItem.Key.Y].Borders[2] = true;
                }
            }

            //if (mctsWeights != null)
            //{
            //    mctsWeights = mctsWeights.Where(w => w.Item1.LastAction != null && w.Item1.LastAction != FieldCoordinates.Pass);
            //
            //    foreach (var item in mctsWeights)
            //    {
            //        var move = item.Item1.LastAction;
            //
            //        if (move != null && move != FieldCoordinates.Pass)
            //        {
            //            boardControl.Fields[move.X, move.Y].AddLabel(Brushes.Blue, $"{item.Item2:f2}");
            //        }
            //    }
            //
            //    var maxItem = mctsWeights.MaxItem(i => i.Item2);
            //    var maxMove = maxItem.Item1.LastAction;
            //
            //    boardControl.Fields[maxMove.X, maxMove.Y].Borders[1] = true;
            //}

            boardControl.Refresh();
        }

        public static void RefreshBoard(GoBoardControl playoutBoardControl, GamePlayoutNode<GameState, FieldCoordinates> node)
        {
            if (node != null)
            {
                for (uint y = 0; y < playoutBoardControl.BoardSize; y++)
                {
                    for (uint x = 0; x < playoutBoardControl.BoardSize; x++)
                    {
                        var field = playoutBoardControl.Fields[x, y];
                        FieldState fieldState = node.State.InternalState.BoardFields[x, y];
                        field.State = fieldState;
                        field.Labels.Clear();
                        field.Label.Text = string.Empty;
                        field.Borders[0] = false;
                        field.Borders[1] = false;
                        field.Borders[2] = false;
                    }
                }

                foreach (var field in node.State.GetAllowedActionsForRandomPlayout())
                {
                    if (field != FieldCoordinates.Pass)
                    {
                        playoutBoardControl.Fields[field.X, field.Y].Borders[2] = true;
                    }
                }

                var path = node.GetPath();
                int num = 1;

                foreach (var item in path)
                {
                    if (item.LastAction != null && item.LastAction != FieldCoordinates.Pass)
                    {
                        var field = playoutBoardControl.Fields[item.LastAction.X, item.LastAction.Y];
                        //field.State = item.GameState.CurrentPlayer.Opposite.Color.State;

                        switch (item.Type)
                        {
                            case GamePlayoutNodeType.Selected:
                                field.Borders[0] = true;
                                break;
                            case GamePlayoutNodeType.Expanded:
                                field.Borders[1] = true;
                                break;
                            case GamePlayoutNodeType.Playout:
                                if (field.Label.Text == string.Empty)
                                {
                                    field.Label.Text = $"{num++}";
                                }
                                else
                                {
                                    field.Label.Text = $",{num++}";
                                }
                                break;
                        }
                    }
                }

                //if (selectedRound.Value.Path != null)
                //{
                //
                //}
                //
                //foreach (var item in selectedRound.Value.Selection.Where(p => p.LastAction != null && p.LastAction != FieldCoordinates.Empty))
                //{
                //    var field = playoutBoardControl.Fields[item.LastAction.X, item.LastAction.Y];
                //    field.State = item.GameState.CurrentPlayer.Opposite.Color.State;
                //    field.Borders[0] = true;
                //}
                //
                //if (selectedRound.Value.Expansion != null)
                //{
                //    var node = selectedRound.Value.Expansion;
                //    var field = playoutBoardControl.Fields[node.LastAction.X, node.LastAction.Y];
                //    field.State = node.GameState.CurrentPlayer.Opposite.Color.State;
                //    field.Borders[1] = true;
                //}
                //
                //int num = 1;
                //
                //foreach (var item in selectedRound.Value.Playout.Where(p => p.Item1 != null && p.Item1 != FieldCoordinates.Empty))
                //{
                //    var field = playoutBoardControl.Fields[item.Item1.X, item.Item1.Y];
                //    field.State = item.Item2.CurrentPlayer.Opposite.Color.State;
                //    field.AddLabel(Brushes.Gray, $"{num}");
                //    num++;
                //}

                playoutBoardControl.Refresh();
            }
        }
    }
}
