using Demos.Forms.Base;
using System.ComponentModel;

namespace Demos.Forms.Utilities
{
    public class TrainingSetConverter<TOutput> : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            var properties = context.Instance as NeuralNetworkDemoFormProperties<TOutput>;
            return new StandardValuesCollection(properties.TrainingSets);
        }
    }
}
