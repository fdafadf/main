using AI.MonteCarlo;
using Games.Go;
using Games.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using GoMctsRound = AI.MonteCarlo.MCTreeSearchRound<AI.MonteCarlo.MCTreeSearchNode<Games.Go.GameState, Games.Go.FieldCoordinates>, Games.Go.GameState, Games.Go.FieldCoordinates>;

namespace Demos.Forms.Go.Game
{
    public class Utilities
    {
        public static void EnsurePathAndNavigate(object sender, ObservableGameTreeNavigator<GamePlayoutNavigator<GameState, FieldCoordinates, Stone>, GameState, FieldCoordinates, Stone, GamePlayoutNode<GameState, FieldCoordinates>> navigator, GoMctsRound round)
        {
            List<FieldCoordinates> pathForward = new List<FieldCoordinates>();
            var parentNode = navigator.Navigator.GameTree.Root;
            
            void AddNode(GameState gameState, FieldCoordinates lastAction, GamePlayoutNodeType type)
            {
                var childNode = navigator.Navigator.GameTree.CreatePlayoutNode(type, gameState, lastAction, parentNode);
                parentNode = navigator.Navigator.GameTree.Expand(parentNode, childNode);
                pathForward.Add(lastAction);
            }
            
            void AddNode2(MCTreeSearchNode<GameState, FieldCoordinates> node, GamePlayoutNodeType type)
            {
                if (node != null)
                {
                    AddNode(node.State, node.LastAction, type);
                }
            }
            
            void AddNodes(IEnumerable<MCTreeSearchNode<GameState, FieldCoordinates>> nodes, GamePlayoutNodeType type)
            {
                if (nodes != null)
                {
                    foreach (var node in nodes)
                    {
                        AddNode(node.State, node.LastAction, type);
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
