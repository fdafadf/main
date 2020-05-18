using AI;
using AI.MonteCarlo;
using AI.NeuralNetworks;
using AI.NeuralNetworks.TicTacToe;
using AI.TicTacToe;
using Core.NetFramework;
using Games.TicTacToe;
using Games.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using static Games.Utilities.StaticUtility;

namespace Demo
{
    class PVNetworkTest
    {
        public TicTacToeValueNetwork CreateNetwork()
        {
            return new TicTacToeValueNetwork(new int[] { 72, 72, 72, 36, 36, 36, 18, 18 }, FunctionName.ReLU, new Random(Guid.NewGuid().GetHashCode()));
        }

        public TicTacToeValueNetwork CreateTrainedNetwork()
        {
            var trainingData = TicTacToeValueLoader.LoadAllUniqueStates(Storage.Instance);
            var trainer = new TicTacToeValueNetworkTrainer(trainingData, 0);
            return trainer.Train(30);
        }

        public void Test()
        {
            var networkA = CreateNetwork();
            var networkB = CreateNetwork();
            int ties = 0;
            int winsA = 0;
            int winsB = 0;

            void MatchAndCount(bool isNetworkACross)
            {
                Player winner;

                if (isNetworkACross)
                {
                    winner = Match(nought: networkB, cross: networkA);
                }
                else
                {
                    winner = Match(nought: networkA, cross: networkB);
                }

                if (winner == null)
                {
                    ties++;
                }
                else if (winner.IsCross)
                {
                    if (isNetworkACross)
                    {
                        winsA++;
                    }
                    else
                    {
                        winsB++;
                    }
                }
                else
                {
                    if (isNetworkACross)
                    {
                        winsB++;
                    }
                    else
                    {
                        winsA++;
                    }
                }
            }

            for (int i = 0; i < 10; i++)
            {
                Console.Title = $"{i}";
                MatchAndCount(true);
                MatchAndCount(false);
            }

            Console.WriteLine($"A: {winsA}  B: {winsB}  Ties: {ties}");
            Console.ReadLine();
        }

        static Player Match(TicTacToeValueNetwork nought, TicTacToeValueNetwork cross)
        {
            var noughtNavigator = new VNetworkMCTreeSearchNavigator(nought);
            var crossNavigator = new VNetworkMCTreeSearchNavigator(cross);
            var activeNavigator = noughtNavigator;
            var inactiveNavigator = crossNavigator;

            while (activeNavigator.CurrentNode.State.IsFinal == false)
            {
                activeNavigator.Round(50);

                Console.WriteLine(ToString(activeNavigator.CurrentNode));
                Console.WriteLine();

                var node = activeNavigator.CurrentNode.GetMostVisitedChild();
                activeNavigator.Play(node.LastAction);
                inactiveNavigator.Play(node.LastAction);
                Swap(ref activeNavigator, ref inactiveNavigator);
            }

            Console.ReadLine();
            return activeNavigator.CurrentNode.State.GetWinner();

            //var expanderA = new PVNetworkBasedMCTreeSearchExpander<TicTacToeGame, GameState, GameAction, Player>(TicTacToeGame.Instance, new Random(0), networkA);
            //var mctsA = new PVNetworkBasedMCTreeSearch<TicTacToeGame, GameState, GameAction, Player>(expanderA);
            //var rootStateA = new GameState();
            //var nodeA = PVNetworkBasedMCTreeSearchNode<GameState, GameAction>.CreateRoot(rootStateA, networkA.);

            //mctsA.Round()
        }

        public static string ToString(MCTreeSearchNode<GameState, GameAction> node)
        {
            string[] lines = node.State.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            for (ushort y = 0; y < 3; y++)
            {
                lines[y] += "   ";

                for (ushort x = 0; x < 3; x++)
                {
                    var action = new GameAction(new BoardCoordinates(x, y));

                    if (node.Children.TryGetValue(action, out var child))
                    {
                        lines[y] += $" {child.Visits,3}";
                    }
                    else
                    {
                        lines[y] += $" ---";
                    }
                }
            }

            for (ushort y = 0; y < 3; y++)
            {
                lines[y] += "   ";

                for (ushort x = 0; x < 3; x++)
                {
                    var action = new GameAction(new BoardCoordinates(x, y));

                    if (node.Children.TryGetValue(action, out var child))
                    {
                        lines[y] += $" {child.Value,5:f2}";
                    }
                    else
                    {
                        lines[y] += $" -----";
                    }
                }
            }

            double wonX;
            double wonO;

            if (node.State.CurrentPlayer.IsCross)
            {
                wonX = node.Children.Select(p => p.Value.Value / p.Value.Visits).Max();
                wonO = 1.0 - node.Children.Select(p => p.Value.Value / p.Value.Visits).Min();
            }
            else
            {
                wonX = 1.0 - node.Children.Select(p => p.Value.Value / p.Value.Visits).Min();
                wonO = node.Children.Select(p => p.Value.Value / p.Value.Visits).Max(); 
            }

            lines[0] += $"   X: {wonX}";
            lines[1] += $"   O: {wonO}";

            return string.Join(Environment.NewLine, lines);
        }
    }

    public class VNetworkMCTreeSearchNavigator : MCTreeSearchNavigator<VNetworkBasedMCTreeSearch, TicTacToeGame, MCTreeSearchNode<GameState, GameAction>, GameState, GameAction, Player>
    {
        public VNetworkMCTreeSearchNavigator(VNetworkBasedMCTreeSearch mcts, MCTreeSearchNode<GameState, GameAction> rootNode) : base(mcts, TicTacToeGame.Instance, rootNode)
        {
        }

        public VNetworkMCTreeSearchNavigator(TicTacToeValueNetwork network) : base(new VNetworkBasedMCTreeSearch(network), TicTacToeGame.Instance, new MCTreeSearchNode<GameState, GameAction>(new GameState()))
        {
        }
    }

    public class VNetworkBasedMCTreeSearch : MCTreeSearchBase<MCTreeSearchNode<GameState, GameAction>, GameState, GameAction, Player>
    {
        TicTacToeValueNetwork Network;

        public VNetworkBasedMCTreeSearch(TicTacToeValueNetwork network) : base(new VNetworkBasedMCTreeSearchExpander(TicTacToeGame.Instance))
        {
            Network = network;
        }

        protected override double Playout(MCTreeSearchNode<GameState, GameAction> leafNode)
        {
            return Network.Evaluate(leafNode.State)[leafNode.State.CurrentPlayer];
        }

        protected override double Playout(MCTreeSearchNode<GameState, GameAction> leafNode, out IEnumerable<Tuple<GameAction, GameState>> path)
        {
            throw new NotImplementedException();
        }
    }

    public class VNetworkBasedMCTreeSearchExpander : MCTreeSearchExpanderBase<TicTacToeGame, MCTreeSearchNode<GameState, GameAction>, GameState, GameAction, Player>
    {
        public VNetworkBasedMCTreeSearchExpander(TicTacToeGame game) : base(game)
        {
        }

        protected override MCTreeSearchNode<GameState, GameAction> CreateNode(MCTreeSearchNode<GameState, GameAction> parentNode, GameState gameState, GameAction action)
        {
            return new MCTreeSearchNode<GameState, GameAction>(parentNode, gameState, action);
        }

        protected override MCTreeSearchNode<GameState, GameAction> SelectExpanded(MCTreeSearchNode<GameState, GameAction> node)
        {
            // return node.Children.Values.MaxItem(child => child.SelectionRating);
            return node.Children.First().Value;
        }
    }

    public class VNetwork : IActionGenerator<GameState, GameAction, Player>
    {
        public GameAction GenerateAction(GameState gameState)
        {
            return null;
        }
    }

    public class TicTacToePVNetwork : IPVNetwork<GameState, GameAction>
    {
        Network Network { get; }

        public TicTacToePVNetwork()
        {
            Network = NetworkBuilder.Build(new NetworkDefinition(FunctionName.ReLU, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), new He(0));
        }

        public PVNetworkOutput<GameAction> Predict(GameState state)
        {
            double[] input = TicTacToeLabeledStateLoader.InputTransforms.Bipolar(state);
            double[] output = Network.Evaluate(input);
            return new TicTacToePVNetworkOutput(output);
        }

        public IEnumerable<PVNetworkOutput<GameAction>> Predict(IEnumerable<GameState> states)
        {
            return states.Select(Predict);
        }
    }

    /*
            Random random = new Random(0);
            NeuralNetwork2 network = new NeuralNetwork2(2, new[] { 5, 1 }, random);
            network.WriteWeights(Console.Out);
            Console.WriteLine();

            double[][] inputs =
            {
                new [] { -1.0, -1.0 },
                new [] { -1.0, +1.0 },
                new [] { +1.0, -1.0 },
                new [] { +1.0, +1.0 },
            };

            double[][] outputs =
            {
                new [] { 0.0 },
                new [] { 1.0 },
                new [] { 1.0 },
                new [] { 0.0 },
            };

            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < inputs.Length; j++)
                {
                    network.Train(inputs[j], outputs[j]);
                }
            }

            for (int j = 0; j < inputs.Length; j++)
            {
                network.Evaluate(inputs[j]);
                Console.WriteLine(network.Output[0]);
            }
            */

    //var fullTree = new TicTacToeGameStateGenerator().FullTree();
    //var fullTreeFlatten = fullTree.Flatten();
    //Dictionary<int, GameState> uniqueStates = new Dictionary<int, GameState>();
    //
    //foreach (var gameState in fullTreeFlatten)
    //{
    //    int hashCode = gameState.GetHashCode();
    //
    //    if (uniqueStates.ContainsKey(hashCode) == false)
    //    {
    //        uniqueStates.Add(hashCode, gameState);
    //    }
    //}
    //
    //Console.WriteLine("All possible states: {0}", fullTreeFlatten.Count());
    //Console.WriteLine("Unique states: {0}", uniqueStates.Count());

    //var test = new PythonInterop().GetAllUniqueStates();

    //   var inputFunction = TicTacToeNeuralIO.InputFunctions.Bipolar;
    //var outputFunction = new TicTacToeMinMaxWinnerEvaluator();
    //var trainingData = TicTacToeNeuralIOGenerator.Instance.GetAllUniqueStates(inputFunction, outputFunction);
    //var csvLines = trainingData.Select(d => d.Input.Concatenate(",") + "," + d.Output.Concatenate(","));
    //File.WriteAllLines(@"C:\Deployment\TicTacToeTrainingData.txt", csvLines);

}
