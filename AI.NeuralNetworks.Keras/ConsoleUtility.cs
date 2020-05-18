using AI.MonteCarlo;
using Games;
using Games.TicTacToe;
using System;
using System.IO;
using System.Linq;

namespace AI.Keras
{
    public static class ConsoleUtility
    {
        public static void WriteLine(ConsoleColor color, string format)
        {
            ConsoleColor originalForegroundColor = Console.ForegroundColor;

            try
            {
                Console.ForegroundColor = color;
                Console.WriteLine(format);
            }
            finally
            {
                Console.ForegroundColor = originalForegroundColor;
            }
        }

        public static void WriteLine<TState, TAction>(PVNetworkBasedMCTreeSearchNode<TState, TAction> node)
            where TState : IPeriodState
        {
            var tnode = node as PVNetworkBasedMCTreeSearchNode<GameState, GameAction>;

            if (tnode != null)
            {
                Console.WriteLine(tnode.State.CurrentPlayer);
                Console.WriteLine(tnode.State);

                for (ushort y = 0; y < 3; y++)
                {
                    for (ushort x = 0; x < 3; x++)
                    {
                        PVNetworkBasedMCTreeSearchNode<GameState, GameAction> c = null;
                        tnode.Children?.TryGetValue(new GameAction(x, y), out c);
                        uint visited = c?.Visits ?? 0;
                        Console.Write($"{visited,5}  ");
                    }

                    Console.WriteLine();
                }

                for (ushort y = 0; y < 3; y++)
                {
                    for (ushort x = 0; x < 3; x++)
                    {
                        PVNetworkBasedMCTreeSearchNode<GameState, GameAction> c = null;
                        tnode.Children?.TryGetValue(new GameAction(x, y), out c);
                        double totalReward = c?.Value ?? 0;
                        Console.Write($"{totalReward,5:f1}  ");
                    }

                    Console.WriteLine();
                }
            }
        }

        public static void WriteLine<TState, TAction>(PVNetworkBasedMCTreeSearchNode<TState, TAction> node, double[] trainingOutput)
            where TState : IPeriodState
        {
            WriteLine(node);

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    Console.Write($"{trainingOutput[x + y * 3]:f3}  ");
                }

                Console.WriteLine();
            }

            Console.WriteLine($"{trainingOutput[9]:f3}");
        }

        public static void WriteTo<TNode>(this TNode node, TextWriter @out)
            where TNode : MCTreeNode<TNode, GameState, GameAction>
        {
            @out.WriteLine(node.State);
            //@out.WriteLine(Path.Last().State);
            //@out.WriteLine($"Selection");
            //Selection.ForEach(n => @out.WriteLine(n.State));
            //@out.WriteLine($"Expansion");
            //@out.WriteLine(Expansion.State);
            //@out.WriteLine($"Playout: {PlayoutValue:f3}");
        }

        public static void WriteTo<TNode>(this MCTreeSearchRound<TNode, GameState, GameAction> round, TextWriter @out)
            where TNode : MCTreeNode<TNode, GameState, GameAction>
        {
            @out.WriteLine($"Path");
            @out.WriteLine(round.Path.Last().State);
            @out.WriteLine($"Selection");
            foreach (TNode node in round.Selection) { node.WriteTo(@out); }
            @out.WriteLine($"Expansion");
            @out.WriteLine(round.Expansion.State);
            @out.WriteLine($"Playout: {round.PlayoutValue:f3}");
        }
    }
}
