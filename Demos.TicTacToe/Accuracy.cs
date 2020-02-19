namespace TicTacToe
{
    public class Accuracy
    {
        public int CorrectPredictions;
        public int TestingSetSize;
        public double Value;

        public Accuracy(int correctPredictions, int testingSetSize)
        {
            CorrectPredictions = correctPredictions;
            TestingSetSize = testingSetSize;
            Value = (double)correctPredictions / testingSetSize;
        }
    }
}
