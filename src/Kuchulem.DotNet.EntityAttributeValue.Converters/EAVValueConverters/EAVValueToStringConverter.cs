using Kuchulem.DotNet.EntityAttributeValue.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuchulem.DotNet.EntityAttributeValue.Converters.EAVValueConverters
{
    public class EAVValueToStringConverter : IEAVValueConverter<string>
    {
        public string Convert(string value)
        {
            return value;
        }

        public string ConvertBack(string value)
        {
            return value;
        }
    }
}
