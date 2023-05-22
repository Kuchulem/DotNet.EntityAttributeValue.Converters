using Kuchulem.DotNet.EntityAttributeValue.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kuchulem.DotNet.EntityAttributeValue.Converters.EAVValueConverters
{
    public class EAVValueListToRawValueConverter : IEAVValueListToRawValueConverter
    {
        public string Convert(IEnumerable<string> values)
        {
            return JsonSerializer.Serialize(values);
        }

        public IEnumerable<string> ConvertBack(string input)
        {
            return JsonSerializer.Deserialize<IEnumerable<string>>(input)
                ?? throw new Exception("Invalid value");
        }
    }
}
