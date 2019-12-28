﻿using Games;
using Games.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.MonteCarlo
{
    public class MCTreeSearch<TGame, TGameState, TGameAction, TPlayer>
        where TGame : IGame<TGameState, TGameAction, TPlayer>
        where TGameState : IGameState<TPlayer>
        where TPlayer : IPlayer
        where TGameAction : IGameAction
    {
        public static readonly double UCT_C = Math.Sqrt(2);

        private TGame Game;
        public MCTreeNode<TGameAction, TGameState> CurrentNode { get; private set; }
        private Action<MCTreeNode<TGameAction, TGameState>> PriorFunction;
        private IGamePlayoutGenerator<TGameState, TPlayer> PlayoutGenerator;
        private Random Random;

        public MCTreeSearch(TGame game, TGameState startState, int seed)
        {
            Random = new Random(seed);
            Game = game;
            PlayoutGenerator = new RandomGamePlayoutGenerator<TGameState, TGameAction, TPlayer>(Game, new Random(seed));
            PriorFunction = a => { };
            CurrentNode = new MCTreeNode<TGameAction, TGameState>(null, startState);
        }

        public bool Move(TGameAction action)
        {
            Expand(CurrentNode);
            MCTreeNode<TGameAction, TGameState> newNode = CurrentNode.Children[action];

            if (newNode == null)
            {
                return false;
            }
            else
            {
                CurrentNode = newNode;
                return true;
            }
        }

        public bool Undo()
        {
            if (CurrentNode.Parent == null)
            {
                return false;
            }
            else
            {
                CurrentNode = CurrentNode.Parent;
                return true;
            }
        }

        //public MCTreeNode<TGameState> RootNode
        //{
        //    get
        //    {
        //        return this.rootNode;
        //    }
        //}

        //public void Round()
        //{
        //    this.Round(this.rootNode);
        //}

        public void Rounds(int number)
        {
            Rounds(this.CurrentNode, number);
        }

        public void Rounds(MCTreeNode<TGameAction, TGameState> node, int number)
        {
            for (int i = 0; i < number; i++)
            {
                Round(node);
            }
        }

        /*
        public MCTreeSearchRound<TGameAction, TGameState> RoundWithDetails(MCTreeNode<TGameAction, TGameState> node)
        {
            MCTreeSearchRound<TGameAction, TGameState> result = new MCTreeSearchRound<TGameAction, TGameState>();
            result.Selection = this.SelectPath(node);
            var selectedNode = result.Selection.Last();

            if (this.Expand(selectedNode))
            {
                result.Expansion = selectedNode.Children.Random().Value;
                var playoutFinalState = this.playoutGenerator.Generate(result.Expansion.Data);

                if (this.game.IsWinner(playoutFinalState, result.Expansion.Data.CurrentPlayer))
                {
                    this.PropagateWin(result.Expansion);
                }
                else
                {
                    this.PropagateLoss(result.Expansion);
                }
            }
            else
            {
                var playoutFinalState = this.playoutGenerator.Generate(selectedNode.Data);

                if (this.game.IsLooser(playoutFinalState, node.Data.CurrentPlayer))
                {
                    this.PropagateLoss(selectedNode);
                }
                else
                {
                    this.PropagateWin(selectedNode);
                }
            }

            return result;
        }
        */

        public void Round(MCTreeNode<TGameAction, TGameState> node)
        {
            var selectedNode = Select(node);

            if (Expand(selectedNode))
            {
                var expandedNode = Random.Next(selectedNode.Children).Value;
                var playoutFinalState = PlayoutGenerator.Generate(expandedNode.Data);

                
                if (expandedNode.Data.CurrentPlayer.Equals(playoutFinalState.GetWinner()))
                {
                    //Console.WriteLine(playoutFinalState);
                    //Console.WriteLine("PropagateWin+");
                    //this.PropagateWin(expandedNode);
                    Propagate(expandedNode, true);
                }
                else
                {
                    //Console.WriteLine(playoutFinalState);
                    //Console.WriteLine("PropagateLoss+");
                    //this.PropagateLoss(expandedNode);
                    Propagate(expandedNode, false);
                }
            }
            else
            {
                var playoutFinalState = PlayoutGenerator.Generate(selectedNode.Data);

                if (node.Data.CurrentPlayer.Equals(playoutFinalState.GetWinner()))
                {
                    //Console.WriteLine(playoutFinalState);
                    //Console.WriteLine("PropagateWin");
                    //this.PropagateWin(selectedNode);
                    Propagate(selectedNode, true);
                }
                else
                {
                    //Console.WriteLine(playoutFinalState);
                    //Console.WriteLine("PropagateLoss");
                    //this.PropagateLoss(selectedNode);
                    Propagate(selectedNode, false);
                }
            }
        }

        private void Propagate(MCTreeNode<TGameAction, TGameState> node, bool win)
        {
            if (node != null)
            {
                node.Simulations++;

                if (node.Data.IsFinal)
                {
                    win = node.Data.CurrentPlayer.Equals(node.Data.GetWinner());
                }

                if (win)
                {
                    node.Wins++;
                }

                Propagate(node.Parent, win == false);
            }
        }

        private void PropagateWin(MCTreeNode<TGameAction, TGameState> node)
        {
            if (node != null)
            {
                node.Simulations++;
                node.Wins++;
                PropagateLoss(node.Parent);
            }
        }

        private void PropagateLoss(MCTreeNode<TGameAction, TGameState> node)
        {
            if (node != null)
            {
                node.Simulations++;
                PropagateWin(node.Parent);
            }
        }

        private MCTreeNode<TGameAction, TGameState> Select(MCTreeNode<TGameAction, TGameState> node)
        {
            return SelectUCT(node);
        }

        private IEnumerable<MCTreeNode<TGameAction, TGameState>> SelectPath(MCTreeNode<TGameAction, TGameState> node)
        {
            return SelectUCTPath(node);
        }

        private bool Expand(MCTreeNode<TGameAction, TGameState> node)
        {
            if (node.IsExpandedAndHasNoChildren)
            {
                return false;
            }

            if (node.IsUnexpanded)
            {
                node.Children = new Dictionary<TGameAction, MCTreeNode<TGameAction, TGameState>>();

                foreach (TGameAction action in Game.GetAllowedActions(node.Data))
                {
                    TGameState state = Game.Play(node.Data, action);
                    node.Children.Add(action, CreateNode(node, state));
                }
            }

            return node.IsExpandedAndHasNoChildren == false;
        }

        private MCTreeNode<TGameAction, TGameState> CreateNode(MCTreeNode<TGameAction, TGameState> parent, TGameState data)
        {
            var result = new MCTreeNode<TGameAction, TGameState>(parent, data);
            PriorFunction(result);
            return result;
        }

        private MCTreeNode<TGameAction, TGameState> SelectUCT(MCTreeNode<TGameAction, TGameState> node)
        {
            if (node.IsUnexpanded || node.IsExpandedAndHasNoChildren)
            {
                return node;
            }
            else
            {
                double logT = Math.Log(node.Simulations);
                MCTreeNode<TGameAction, TGameState> maxNode = node.Children.Values.MaxItem(n => CalculateUCTWeight(n, logT));
                return SelectUCT(maxNode);
            }
        }

        private LinkedList<MCTreeNode<TGameAction, TGameState>> SelectUCTPath(MCTreeNode<TGameAction, TGameState> node)
        {
            LinkedList<MCTreeNode<TGameAction, TGameState>> result;

            if (node.IsUnexpanded || node.IsExpandedAndHasNoChildren)
            {
                result = new LinkedList<MCTreeNode<TGameAction, TGameState>>();
            }
            else
            {
                double logT = Math.Log(node.Simulations);
                MCTreeNode<TGameAction, TGameState> maxNode = node.Children.Values.MaxItem(n => CalculateUCTWeight(n, logT));
                result = SelectUCTPath(maxNode);
            }

            result.AddFirst(node);
            return result;
        }

        private double CalculateUCTWeight(MCTreeNode<TGameAction, TGameState> node, double logT)
        {
            if (node.Simulations == 0)
            {
                return double.PositiveInfinity;
            }
            else
            {
                double s1 = (double)node.Wins / node.Simulations;
                double s2 = UCT_C * Math.Sqrt(logT / node.Simulations);
                return s1 + s2;
            }
        }

        public static string CalculateUCT(MCTreeNode<TGameAction, TGameState> node)
        {
            if (node.Simulations == 0)
            {
                return double.PositiveInfinity.ToString();
            }
            else
            {
                double logT = Math.Log(node.Parent.Simulations);
                double s1 = (double)node.Wins / node.Simulations;
                double s2 = UCT_C * Math.Sqrt(logT / node.Simulations);
                return $@"s1({s1})s2({s2})logT({logT})";
            }
        }
    }
}
