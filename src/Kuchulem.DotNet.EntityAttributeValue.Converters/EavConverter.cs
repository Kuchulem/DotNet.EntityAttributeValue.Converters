using Kuchulem.DotNet.EntityAttributeValue.Abstractions;
using Kuchulem.DotNet.EntityAttributeValue.Converters.EAVValueConverters;
using Kuchulem.DotNet.EntityAttributeValue.Converters.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuchulem.DotNet.EntityAttributeValue.Converters
{
    /// <summary>
    /// Implementation of <see cref="IEavConverter"/>.<br/>
    /// The implementation expects an implementation of <see cref="IEavRawValueConverterProvider"/>
    /// upon construction (With DI for exemple) to find the <see cref="IEavRawValueConverter"/>
    /// to apply to a value.<br/>
    /// The <see cref="EavRawValueConverterProvider"/> implementation can be used but developpers
    /// can use their own implementation.<br/>
    /// See the extension methods of <see cref="IServiceCollectionExtensions"/> for more information.
    /// </summary>
    public class EavConverter : IEavConverter
    {
        private readonly IEavRawValueConverterProvider converterProvider;

        /// <summary>
        /// Constructor, expecting the <see cref="IEavRawValueConverterProvider"/> implementation
        /// </summary>
        /// <param name="converterProvider"></param>
        public EavConverter(IEavRawValueConverterProvider converterProvider)
        {
            this.converterProvider = converterProvider;
        }

        /// <summary>
        /// Converts the raw value from <see cref="IEavValue.RawValue"/> to
        /// the referenced parameter <em>converted</em>.
        /// Returns true if conversion was done, false if no converter
        /// was found and throws an exception if no attribute is provided
        /// by the <see cref="IEavValue.GetEavAttribute"/> method.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="converted"></param>
        /// <returns></returns>
        public bool TryConvertValue(IEavValue value, out object? converted)
        {
            converted = default;

            if (value.GetEavAttribute() is not IEavAttribute attribute)
                throw new Exception("No attribute found");

            var toConvert = value.RawValue ?? "";

            if (converterProvider.TryGetConverter(attribute, out var converter) && converter is not null)
            {
                converted = converter.Convert(toConvert);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Tries to convert the values of an entity attributes.<br/>
        /// When multiple <see cref="IEavValue"/> are found for a single
        /// <see cref="IEavAttribute"/> only the first one is used as value
        /// unless the <see cref="IEavAttribute.ValueListKind"/> property
        /// is <see cref="EavValueListKind.Multiple"/>. In that case the
        /// conversion result is an <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="entity"></param>
        /// <param name="converted"></param>
        /// <returns></returns>
        public bool TryConvertEntityAttributeValue(IEavAttribute attribute, IEavEntity entity, out object? converted)
        {
            var values = entity.GetEavValues().Where(v => v.GetEavAttribute()?.AttributeName == attribute.AttributeName);

            converted = default;

            if (attribute.ValueListKind != EavValueListKind.Multiple)
            {
                if (!values.Any())
                    return false;

                return TryConvertValue(values.First(), out converted);
            }

            List<object> convertedList = new();

            foreach(var value in values)
            {
                if(TryConvertValue(value, out var convertedValue) && convertedValue is not null)
                    convertedList.Add(convertedValue);
            }

            converted = convertedList.AsEnumerable();

            return true;
        }

        /// <summary>
        /// Converts all <see cref="IEavValue"/> for all <see cref="IEavAttribute"/> of
        /// an entity and populates a <see cref="Dictionary{TKey, TValue}"/> with
        /// the attribute as key and the convertion result as value.<br/>
        /// When multiple <see cref="IEavValue"/> are found for a single
        /// <see cref="IEavAttribute"/> only the first one is used as value
        /// unless the <see cref="IEavAttribute.ValueListKind"/> property
        /// is <see cref="EavValueListKind.Multiple"/>. In that case the
        /// convertion result is an <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="converted"></param>
        /// <returns></returns>
        public bool TryConvertEntityValues(IEavEntity entity, out Dictionary<IEavAttribute, object?> converted)
        {
            converted = new Dictionary<IEavAttribute, object?>();

            var attributes = entity.GetEavValues().Select(v => v.GetEavAttribute()).Distinct();

            foreach (var attribute in attributes)
            {
                if (attribute is not null && TryConvertEntityAttributeValue(attribute, entity, out object? convertedValue))
                    converted.Add(attribute, convertedValue);
                else
                {
                    converted.Clear();
                    return false;
                }
            }

            return true;
        }
    }
}
