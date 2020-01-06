using Games;
using Games.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.MonteCarlo
{
    public class MCTreeSearch<TGame, TGameState, TGameAction, TPlayer>
        where TGame : IGame<TGameState, TGameAction, TPlayer>
        where TGameState : IGameState<TPlayer>
        where TPlayer : IPlayer
        where TGameAction : class, IGameAction
    {
        public static readonly double UCT_C = Math.Sqrt(2);

        private TGame Game;
        public TPlayer Player { get; }
        public MCTreeNode<TGameAction, TGameState> CurrentNode { get; private set; }
        public MCTreeNode<TGameAction, TGameState> Root { get; set; }

        private Action<MCTreeNode<TGameAction, TGameState>> PriorFunction;
        public IGamePlayoutGenerator<TGameState, TPlayer, TGameAction> PlayoutGenerator { get; }
        private Random Random;

        public MCTreeSearch(TGame game, TPlayer player, TGameState startState, int seed)
        {
            Random = new Random(seed);
            Game = game;
            Player = player;
            PlayoutGenerator = new GamePlayoutRandomGenerator<TGameState, TPlayer, TGameAction>(Game, new Random(seed));
            PriorFunction = a => { };
            Root = new MCTreeNode<TGameAction, TGameState>(null, startState, null);
            CurrentNode = Root;
        }

        public MCTreeSearch(TGame game, TPlayer player, TGameState startState, Random random, Func<TGameState, TGameAction> randomActionProvider)
        {
            Random = random;
            Game = game;
            Player = player;
            PlayoutGenerator = new GamePlayoutRandomGenerator<TGameState, TPlayer, TGameAction>(Game, randomActionProvider);
            PriorFunction = a => { };
            Root = new MCTreeNode<TGameAction, TGameState>(null, startState, null);
            CurrentNode = Root;
        }

        public bool Play(TGameAction action)
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
        
        public void Round(int repeats)
        {
            Round(CurrentNode, repeats);
        }

        public void Round(MCTreeNode<TGameAction, TGameState> node, int repeats)
        {
            for (int i = 0; i < repeats; i++)
            {
                Round(node);
            }
        }

        public void Round(MCTreeNode<TGameAction, TGameState> node)
        {
            TGameState playoutFinalState;
            MCTreeNode<TGameAction, TGameState> selectedNode = Select(node);

            if (selectedNode.GameState.IsFinal)
            {
                playoutFinalState = selectedNode.GameState;
            }
            else
            {
                if (Expand(selectedNode))
                {
                    selectedNode = Random.Next(selectedNode.Children).Value;
                }

                playoutFinalState = PlayoutGenerator.Generate(selectedNode.GameState);
            }

            Propagate(playoutFinalState, selectedNode);
        }

        public IEnumerable<MCTreeSearchRound<TGameAction, TGameState>> RoundWithDetails(int repeats)
        {
            List<MCTreeSearchRound<TGameAction, TGameState>> result = new List<MCTreeSearchRound<TGameAction, TGameState>>();

            for (int i = 0; i < repeats; i++)
            {
                result.Add(RoundWithDetails(CurrentNode));
            }

            return result;
        }

        public MCTreeSearchRound<TGameAction, TGameState> RoundWithDetails()
        {
            return RoundWithDetails(CurrentNode);
        }

        public MCTreeSearchRound<TGameAction, TGameState> RoundWithDetails(MCTreeNode<TGameAction, TGameState> node)
        {
            MCTreeSearchRound<TGameAction, TGameState> result = new MCTreeSearchRound<TGameAction, TGameState>();
            result.Path = GameTreeNode<TGameState, TGameAction, MCTreeNode<TGameAction, TGameState>>.GetPath(node);
            result.Selection = SelectPath(node);
            var selectedNode = result.Selection.Last();
            TGameState playoutFinalState;

            if (selectedNode.GameState.IsFinal)
            {
                playoutFinalState = selectedNode.GameState;
            }
            else
            {
                if (Expand(selectedNode))
                {
                    result.Expansion = Random.Next(selectedNode.Children).Value;
                    selectedNode = result.Expansion;
                }

                result.Playout = PlayoutGenerator.GeneratePath(selectedNode.GameState);

                if (result.Playout.Any())
                {
                    playoutFinalState = result.Playout.Last().Item2;
                }
                else
                {
                    playoutFinalState = selectedNode.GameState;
                }
            }

            Propagate(playoutFinalState, selectedNode);
            return result;
        }

        private void Propagate(TGameState playoutFinalState, MCTreeNode<TGameAction, TGameState> node)
        {
            TPlayer winner = playoutFinalState.GetWinner();

            if (winner == null)
            {
                PropagateDraw(node);
            }
            else if (Player.Equals(winner))
            {
                if (Player.Equals(node.GameState.CurrentPlayer))
                {
                    PropagateLoss(node);
                }
                else
                {
                    PropagateWin(node);
                }
            }
            else
            {
                if (Player.Equals(node.GameState.CurrentPlayer))
                {
                    PropagateWin(node);
                }
                else
                {
                    PropagateLoss(node);
                }
            }
        }

        private void PropagateWin(MCTreeNode<TGameAction, TGameState> node)
        {
            if (node != null)
            {
                node.Simulations += 10;
                node.Wins += 10;
                PropagateLoss(node.Parent);
            }
        }

        private void PropagateLoss(MCTreeNode<TGameAction, TGameState> node)
        {
            if (node != null)
            {
                node.Simulations += 10;
                PropagateWin(node.Parent);
            }
        }

        private void PropagateDraw(MCTreeNode<TGameAction, TGameState> node)
        {
            if (node != null)
            {
                node.Simulations += 10;
                node.Wins += 5;
                PropagateDraw(node.Parent);
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

        public bool Expand(MCTreeNode<TGameAction, TGameState> node)
        {
            if (node.IsExpandedAndHasNoChildren)
            {
                return false;
            }

            if (node.IsUnexpanded)
            {
                node.Children = new Dictionary<TGameAction, MCTreeNode<TGameAction, TGameState>>();

                foreach (TGameAction action in Game.GetAllowedActions(node.GameState))
                {
                    TGameState state = Game.Play(node.GameState, action);
                    node.Children.Add(action, CreateNode(node, state, action));
                }
            }

            return node.IsExpandedAndHasNoChildren == false;
        }

        private MCTreeNode<TGameAction, TGameState> CreateNode(MCTreeNode<TGameAction, TGameState> parent, TGameState data, TGameAction lastAction)
        {
            var result = new MCTreeNode<TGameAction, TGameState>(parent, data, lastAction);
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

        public IEnumerable<Tuple<MCTreeNode<TGameAction, TGameState>, double>> GetChildrenWeights(MCTreeNode<TGameAction, TGameState> node)
        {
            if (node.IsUnexpanded || node.IsExpandedAndHasNoChildren)
            {
                return null;
            }
            else
            {
                double logT = Math.Log(node.Simulations);
                return node.Children.Select(n => Tuple.Create(n.Value, CalculateUCTWeight(n.Value, logT)));
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
