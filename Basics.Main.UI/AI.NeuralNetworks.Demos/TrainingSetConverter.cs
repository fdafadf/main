using System.ComponentModel;

namespace Basics.AI.NeuralNetworks.Demos
{
    public class TrainingSetConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            var properties = context.Instance as NeuralNetworkDemoFormProperties;
            return new StandardValuesCollection(properties.TrainingSets);
        }
    }
}
