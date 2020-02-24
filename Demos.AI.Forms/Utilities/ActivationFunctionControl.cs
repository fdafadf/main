using AI.NeuralNetworks;
using System.Windows.Forms;

namespace Demos.Forms.Utilities
{
    public class ActivationFunctionControl : ComboBox
    {
        public ActivationFunctionControl()
        {
            //Items.Add(ActivationFunctions.BinaryStep);
            //Items.Add(ActivationFunctions.HiperbolicTangens);
            //Items.Add(ActivationFunctions.Sigmoid);
            DropDownStyle = ComboBoxStyle.DropDownList;
        }
    }
}
