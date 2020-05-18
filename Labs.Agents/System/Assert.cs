using System;
using System.Collections.Generic;
using System.Linq;

namespace Labs.Agents
{
    public static class Assert
    {
        public static void NotNullOrWhiteSpace(string value, string exceptionMesage)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(exceptionMesage);
            }
        }

        public static void Unique(string value, IEnumerable<string> values, string exceptionMesage)
        {
            if (values.Contains(value))
            {
                throw new ArgumentException(exceptionMesage);
            }
        }
    }
}
