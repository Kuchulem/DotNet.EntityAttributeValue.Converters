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
    /// Class used to convert a <see cref="IEavValue.RawValue"/> to <see cref="DateTime"/>
    /// and backward.<br/>
    /// This implementation of <see cref="IEavRawValueConverter"/> can be registered in
    /// an implementation of <see cref="IEavRawValueConverterProvider"/> (like
    /// <see cref="EavRawValueConverterProvider"/>) to convert <see cref="IEavValue"/>
    /// with a <see cref="EavValueKind.DateTime"/> property.
    /// </summary>
    public class EavRawValueToDateTimeConverter : IEavRawValueConverter
    {
        /// <summary>
        /// Converts the <see cref="IEavValue.RawValue"/> to <see cref="DateTime"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="InvalidConversionException"></exception>
        public object Convert(string value)
        {
            if(DateTime.TryParse(value, CultureInfo.InvariantCulture, out DateTime result)) 
                return result;

            throw new InvalidConversionException(value, typeof(DateTime));
        }

        /// <summary>
        /// Converts a <see cref="DateTime"/> to <see cref="IEavValue.RawValue"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="InvalidConversionException"></exception>
        public string ConvertBack(object value)
        {
            if(value is DateTime dateTime)
                return dateTime.ToString(CultureInfo.InvariantCulture);

            throw new InvalidConversionException(typeof(DateTime), value.GetType());
        }
    }
}
