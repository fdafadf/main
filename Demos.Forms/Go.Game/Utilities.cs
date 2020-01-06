using AI.MonteCarlo;
using Games.Go;
using Games.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demos.Forms.Go.Game
{
    public class Utilities
    {
        public static void EnsurePathAndNavigate(object sender, GamePlayoutNavigator<GameState, FieldCoordinates, Stone> navigator, MCTreeSearchRound<FieldCoordinates, GameState> round)
        {
            List<FieldCoordinates> pathForward = new List<FieldCoordinates>();
            var parentNode = navigator.GameTree.Root;

            void AddNode(GameState gameState, FieldCoordinates lastAction, GamePlayoutNodeType type)
            {
                var childNode = navigator.GameTree.CreatePlayoutNode(type, gameState, lastAction, parentNode);
                parentNode = navigator.GameTree.Expand(parentNode, childNode);
                pathForward.Add(lastAction);
            }

            void AddNode2(MCTreeNode<FieldCoordinates, GameState> node, GamePlayoutNodeType type)
            {
                if (node != null)
                {
                    AddNode(node.GameState, node.LastAction, type);
                }
            }

            void AddNodes(IEnumerable<MCTreeNode<FieldCoordinates, GameState>> nodes, GamePlayoutNodeType type)
            {
                if (nodes != null)
                {
                    foreach (var node in nodes)
                    {
                        AddNode(node.GameState, node.LastAction, type);
                    }
                }
            }

            void AddNodes2(IEnumerable<Tuple<FieldCoordinates, GameState>> nodes, GamePlayoutNodeType type)
            {
                if (nodes != null)
                {
                    foreach (var node in nodes)
                    {
                        AddNode(node.Item2, node.Item1, type);
                    }
                }
            }

            AddNodes(round.Path?.Skip(1), GamePlayoutNodeType.Path);
            AddNodes(round.Selection, GamePlayoutNodeType.Selected);
            AddNode2(round.Expansion, GamePlayoutNodeType.Expanded);
            AddNodes2(round.Playout, GamePlayoutNodeType.Playout);

            navigator.NavigateFromRoot(sender, pathForward);
        }
    }
}
