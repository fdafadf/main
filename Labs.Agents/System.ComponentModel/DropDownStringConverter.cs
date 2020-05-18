using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Labs.Agents.ComponentModel
{
    public class DropDownStringConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) 
        { 
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) 
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            string propertyName = context.PropertyDescriptor.Name;
            string pupulateMethodName = $"Get{propertyName}s";
            var values = context.Instance.GetType().GetMethod(pupulateMethodName).Invoke(context.Instance, new object[] { }) as IEnumerable<string>;
            return new StandardValuesCollection(values.ToList());
        }
    }
}
