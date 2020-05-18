using AI.NeuralNetworks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace Labs.Agents.ComponentModel
{
    public class CsvTypeConverter : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType)
        {
            if (destType == typeof(string))
            {
                var values = value as List<NetworkLayerDefinition>;

                if (values == null)
                {
                    return string.Empty;
                }
                else
                {
                    return string.Join("; ", values);
                }
            }

            return base.ConvertTo(context, culture, value, destType);
        }
    }
}
