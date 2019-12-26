using System.Windows.Forms;

namespace Basics.AI.NeuralNetworks.Demos.Perceptron
{
    public class ActivationFunctionControl : ComboBox
    {
        public ActivationFunctionControl()
        {
            Items.Add(ActivationFunctions.BinaryStep);
            Items.Add(ActivationFunctions.HiperbolicTangens);
            Items.Add(ActivationFunctions.Sigmoid);
            DropDownStyle = ComboBoxStyle.DropDownList;
        }
    }
}
