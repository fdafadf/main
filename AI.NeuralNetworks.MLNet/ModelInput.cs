using Microsoft.ML.Data;

namespace Basics.MLNet
{
    public class ModelInput
    {
        [ColumnName("1"), LoadColumn(0)]
        public float Col1 { get; set; }


        [ColumnName("2"), LoadColumn(1)]
        public float Col2 { get; set; }


        [ColumnName("3"), LoadColumn(2)]
        public float Col3 { get; set; }


        [ColumnName("4"), LoadColumn(3)]
        public float Col4 { get; set; }


        [ColumnName("5"), LoadColumn(4)]
        public float Col5 { get; set; }


        [ColumnName("6"), LoadColumn(5)]
        public float Col6 { get; set; }


        [ColumnName("7"), LoadColumn(6)]
        public float Col7 { get; set; }


        [ColumnName("8"), LoadColumn(7)]
        public float Col8 { get; set; }


        [ColumnName("9"), LoadColumn(8)]
        public float Col9 { get; set; }


        [ColumnName("R"), LoadColumn(9)]
        public bool R { get; set; }
    }
}
