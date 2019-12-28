using Demos.Forms.Base;
using System.ComponentModel;

namespace Demos.Forms.Utilities
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
