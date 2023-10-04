using Kuchulem.DotNet.EntityAttributeValue.Abstractions;
using Kuchulem.DotNet.EntityAttributeValue.Converters.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuchulem.DotNet.EntityAttributeValue.Converters.EAVValueConverters
{
    /// <summary>
    /// Class used to convert a <see cref="IEavValue.RawValue"/> to <see cref="bool"/>
    /// and backward.<br/>
    /// This implementation of <see cref="IEavRawValueConverter"/> can be registered in
    /// an implementation of <see cref="IEavRawValueConverterProvider"/> (like
    /// <see cref="EavRawValueConverterProvider"/>) to convert <see cref="IEavValue"/>
    /// with a <see cref="EavValueKind.Boolean"/> property.
    /// </summary>
    public class EavRawValueToBoolConverter : IEavRawValueConverter
    {
        /// <summary>
        /// Converts the <see cref="IEavValue.RawValue"/> to <see cref="bool"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="InvalidConversionException"></exception>
        public object Convert(string value)
        {
            if (bool.TryParse(value, out bool result))
                return result;

            throw new InvalidConversionException(value, typeof(bool));
        }

        /// <summary>
        /// Converts a <see cref="bool"/> to <see cref="IEavValue.RawValue"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="InvalidConversionException"></exception>
        public string ConvertBack(object value)
        {
            if(value is bool boolean)
                return boolean.ToString(CultureInfo.InvariantCulture);

            throw new InvalidConversionException(typeof(bool), value.GetType());
        }
    }
}
