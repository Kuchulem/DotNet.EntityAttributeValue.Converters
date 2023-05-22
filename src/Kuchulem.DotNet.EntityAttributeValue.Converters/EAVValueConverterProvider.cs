using Kuchulem.DotNet.EntityAttributeValue.Abstractions;
using Kuchulem.DotNet.EntityAttributeValue.Converters.EAVValueConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Kuchulem.DotNet.EntityAttributeValue.Converters
{
    public class EAVValueConverterProvider : IEAVValueConverterProvider
    {
        private readonly Dictionary<(Type, EAVValueKind), object> genericConverters = new();

        private readonly Dictionary<string, object> attributeConverters = new();

        public void RegisterGenericConverters()
        {
            Register(EAVValueKind.Boolean, new EAVValueToBoolConverter());
            Register(EAVValueKind.DateTime, new EAVValueToDateTimeConverter());
            Register(EAVValueKind.Number, new EAVValueToDoubleConverter());
            Register(EAVValueKind.Number, new EAVValueToFloatConverter());
            Register(EAVValueKind.Integer, new EAVValueToIntConverter());
            Register(EAVValueKind.Integer, new EAVValueToLongConverter());
            Register(EAVValueKind.String, new EAVValueToStringConverter());
        }

        public IEAVValueConverter<T> GetConverter<T>(EAVValueKind valueKind)
        {
            if(!TryGetConverter<T>(valueKind, out var converter) || converter == null)
                throw new Exception(string.Format("No converter registered for this type and value kinds : {0}, {1}", typeof(T), valueKind));

            return converter;
        }

        public IEAVValueConverter<T> GetConverter<T>(EAVAttributeBase attribute)
        {
            if (!TryGetConverter<T>(attribute, out var converter) || converter == null)
                throw new Exception(string.Format("No converter registered for this attribute : {0}, or registered with a different typeparam ({1})", attribute.AttributeName, typeof(T)));

            return converter;
        }

        public IEAVValueConverterProvider Register<T>(EAVValueKind valueKind, IEAVValueConverter<T> converter)
        {
            var type = typeof(T);
            var key = (type, valueKind);

            if(genericConverters.ContainsKey(key))
                throw new Exception(string.Format("A converter is already registered for this type and value kinds : {0}, {1}", key.type, key.valueKind));

            genericConverters[key] = converter;

            return this;
        }

        public IEAVValueConverterProvider Register<T>(EAVAttributeBase attribute, IEAVValueConverter<T> converter)
        {
            var key = attribute.AttributeName
                ?? throw new Exception("Can not register an attribute with null name");

            if (attributeConverters.ContainsKey(key))
                throw new Exception(string.Format("A converter is already registered for this attribute : {0}", key));

            attributeConverters[key] = converter;

            return this;
        }

        public bool TryGetConverter<T>(EAVValueKind valueKind, out IEAVValueConverter<T>? converter)
        {
            var type = typeof(T);
            var key = (type, valueKind);
            converter = null;

            if (!genericConverters.ContainsKey(key))
                return false;

            converter = genericConverters[key] as IEAVValueConverter<T>;

            return converter != null;
        }

        public bool TryGetConverter<T>(EAVAttributeBase attribute, out IEAVValueConverter<T>? converter)
        {
            var key = attribute.AttributeName
                ?? throw new Exception("Can not register an attribute with null name");

            converter = null;

            if (!attributeConverters.ContainsKey(key))
                return false;

            converter = attributeConverters[key] as IEAVValueConverter<T>;

            return converter != null;
        }
    }
}
