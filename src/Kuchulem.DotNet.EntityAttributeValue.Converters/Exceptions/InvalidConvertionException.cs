using Kuchulem.DotNet.EntityAttributeValue.Abstractions;

namespace Kuchulem.DotNet.EntityAttributeValue.Converters.Exceptions
{
    /// <summary>
    /// Exception throw when the converter could not parse a <see cref="IEavValue.RawValue"/> value
    /// or convert a value to an <see cref="IEavValue.RawValue"/>.
    /// </summary>
    public class InvalidConversionException : InvalidCastException
    {
        private const string ConvertMessageFormat = "Can't convert \"{0}\" to {1}";
        private const string ConvertBackMessageFormat = "Can't convert back : expected {0} but received {1}";

        /// <summary>
        /// COnstructor with the rawvalue and type expected from the <see cref="IEavRawValueConverter"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="expectedType"></param>
        public InvalidConversionException(string value, Type expectedType)
            : base(string.Format(ConvertMessageFormat, value, expectedType.Name))
        {
        }

        /// <summary>
        /// Constructor with the expected type for the <see cref="IEavRawValueConverter"/> and the
        /// provided type.
        /// </summary>
        /// <param name="expectedType"></param>
        /// <param name="providedType"></param>
        public InvalidConversionException(Type expectedType, Type providedType)
            : base(string.Format(ConvertBackMessageFormat, expectedType.Name, providedType.Name))
        {
        }
    }
}
