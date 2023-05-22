using Kuchulem.DotNet.EntityAttributeValue.Abstractions;
using Kuchulem.DotNet.EntityAttributeValue.Converters.EAVValueConverters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuchulem.DotNet.EntityAttributeValue.Converters
{
    public class ValueConverter
    {
        private readonly EAVValueConverterProvider converterProvider;

        public ValueConverter(EAVValueConverterProvider converterProvider)
        {
            this.converterProvider = converterProvider;
        }

        public T? Convert<T>(EAVValueBase value)
        {
            if(value.Attribute is not EAVAttributeBase attribute)
                throw new Exception("No attribute provided with the value");

            var toConvert = value.RawValue ?? "";

            if (TryGetConverter<T>(attribute, out var converter) && converter is not null)
                return converter.Convert(toConvert);

            throw new Exception("Could not get converter");
        }

        public IEnumerable<T> ConvertList<T>(EAVValueBase value)
        {
            if (value.Attribute is not EAVAttributeBase attribute)
                throw new Exception("No attribute provided with the value");

            var toConvert = value.RawValue ?? "";

            if (attribute.ValueListKind != EAVValueListKind.Multiple)
                throw new Exception("Not a list value");

            var rawValues = new EAVValueListToRawValueConverter().ConvertBack(toConvert);

            if (TryGetConverter<T>(attribute, out var converter) && converter is not null)
                return DoConvertList(converter, rawValues);

            throw new Exception("Could not get converter");
        }

        private bool TryGetConverter<T>(EAVAttributeBase attribute, out IEAVValueConverter<T>? converter)
        {
            if ((converterProvider.TryGetConverter<T>(attribute, out converter)
                || converterProvider.TryGetConverter<T>(attribute.ValueKind, out converter))
                && converter is not null)
                return true;

            return false;
        }

        private static IEnumerable<T> DoConvertList<T>(IEAVValueConverter<T> converter, IEnumerable<string> rawValues)
        {
            foreach(var rawValue in rawValues)
                yield return converter.Convert(rawValue);
        }
    }
}
