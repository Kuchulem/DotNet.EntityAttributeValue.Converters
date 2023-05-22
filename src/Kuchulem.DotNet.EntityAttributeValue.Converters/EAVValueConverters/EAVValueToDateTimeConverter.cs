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
    public class EAVValueToDateTimeConverter : IEAVValueConverter<DateTime>
    {
        public DateTime Convert(string value)
        {
            if(DateTime.TryParse(value, CultureInfo.InvariantCulture, out DateTime result)) 
                return result;

            throw new InvalidConversionException(value, typeof(DateTime));
        }

        public string ConvertBack(DateTime value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
