using AI.NeuralNetworks.Games;
using AI.NeuralNetworks.TicTacToe;
using Core.NetStandard;
using Games.TicTacToe;
using System;
using System.Linq;

namespace AI.TicTacToe
{
    public class TicTacToeValueLoader
    {
        private static LabeledState<GameState, TicTacToeValue>[] AllUniqueStates;

        public static LabeledState<GameState, TicTacToeValue>[] LoadAllUniqueStates(IStorage storage, Func<GameState, double[]> inputTransform)
        {
            if (AllUniqueStates == null)
            {
                string resourceName = "TicTacToe-LabeledState-AllUniqueStates.txt";

                try
                {
                    AllUniqueStates = storage.Read<LabeledState<GameState, TicTacToeValue>[]>(resourceName);
                }
                catch (Exception)
                {
                    var evaluator = new TicTacToeValueEvaluator(inputTransform);
                    var generator = TicTacToeLabeledStateGenerator<TicTacToeValue>.Instance;
                    AllUniqueStates = generator.GetAllUniqueStates(evaluator).ToArray();

                    try
                    {
                        storage.Write(resourceName, AllUniqueStates);
                    }
                    catch (Exception exception)
                    {
                    }
                }
            }

            return AllUniqueStates;
        }

        public static LabeledState<GameState, TicTacToeValue>[] LoadAllUniqueStates(Func<GameState, double[]> inputTransform)
        {
            var evaluator = new TicTacToeValueEvaluator(inputTransform);
            var generator = TicTacToeLabeledStateGenerator<TicTacToeValue>.Instance;
            return generator.GetAllUniqueStates(evaluator).ToArray();
        }
    }
}
