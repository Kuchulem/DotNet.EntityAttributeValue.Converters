using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuchulem.DotNet.EntityAttributeValue.Converters.Exceptions
{
    public class InvalidConversionException : InvalidCastException
    {
        private const string MessageFormat = "Can't convert \"{0}\" to {1}";

        public InvalidConversionException(string value, Type expectedType)
            : base(string.Format(MessageFormat, value, expectedType.Name))
        {
        }
    }
}
