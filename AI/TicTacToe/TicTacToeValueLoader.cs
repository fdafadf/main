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

        public static LabeledState<GameState, TicTacToeValue>[] LoadAllUniqueStates(IStorage storage)
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
                    var evaluator = new TicTacToeValueEvaluator(TicTacToeLabeledStateLoader.InputTransforms.Bipolar);
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
                //FileInfo featuresCacheFile = new FileInfo(Path.Combine(Application.UserAppDataPath, @"TicTacToe-DataLoader-Features.txt"));
                //FileInfo labelsCacheFile = new FileInfo(Path.Combine(Application.UserAppDataPath, @"TicTacToe-DataLoader-Labels.txt"));

                //if (featuresCacheFile.Exists && labelsCacheFile.Exists)
                //{
                //    testingFeatures = JsonConvert.DeserializeObject<double[][]>(File.ReadAllText(featuresCacheFile.FullName));
                //    testingLabels = JsonConvert.DeserializeObject<TicTacToeResultProbabilities[]>(File.ReadAllText(labelsCacheFile.FullName));
                //}
                //else
                //{
                //    TicTacToeTrainingData.Load(InputTransform, out testingFeatures, out testingLabels);
                //    File.WriteAllText(featuresCacheFile.FullName, JsonConvert.SerializeObject(testingFeatures));
                //    File.WriteAllText(labelsCacheFile.FullName, JsonConvert.SerializeObject(testingLabels));
                //}
                //
                //trainingFeatures = testingFeatures.Clone() as double[][];
                //trainingLabels = testingLabels.Select(o => o.Probabilities).ToArray();



            }

            return AllUniqueStates;

            //inputs = trainingData.Select(d => d.Input).ToArray();
            //expectedPredictions = trainingData.Select(d => d.Label).ToArray();
        }
    }
}
