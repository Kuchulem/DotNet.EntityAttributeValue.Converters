using Kuchulem.DotNet.EntityAttributeValue.Abstractions;
using Kuchulem.DotNet.EntityAttributeValue.Converters.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuchulem.DotNet.EntityAttributeValue.Converters.EAVValueConverters
{
    /// <summary>
    /// Class used to convert a <see cref="IEavValue.RawValue"/> to <see cref="string"/>
    /// and backward.<br/>
    /// This implementation of <see cref="IEavRawValueConverter"/> can be registered in
    /// an implementation of <see cref="IEavRawValueConverterProvider"/> (like
    /// <see cref="EavRawValueConverterProvider"/>) to convert <see cref="IEavValue"/>
    /// with a <see cref="EavValueKind.String"/> property.
    /// </summary>
    public class EavRawValueToStringConverter : IEavRawValueConverter
    {
        /// <summary>
        /// Converts the <see cref="IEavValue.RawValue"/> to <see cref="string"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public object Convert(string value)
        {
            return value;
        }

        /// <summary>
        /// Converts a <see cref="string"/> to <see cref="IEavValue.RawValue"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="InvalidConversionException"></exception>
        public string ConvertBack(object value)
        {
            if(value is string str)
                return str;

            throw new InvalidConversionException(typeof(string), value.GetType());
        }
    }
}
