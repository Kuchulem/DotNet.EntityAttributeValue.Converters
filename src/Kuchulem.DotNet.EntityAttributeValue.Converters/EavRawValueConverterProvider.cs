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
    /// <summary>
    /// Implementation of <see cref="IEavRawValueConverterProvider"/>. This class
    /// registers the <see cref="IEavRawValueConverter"/>s for <see cref="IEavAttribute"/>
    /// matching a validator.<br/>
    /// This implementation provides a method to register the "generic converters"
    /// (<see cref="EavRawValueConverterProvider.RegisterGenericConverters"/>) based on the
    /// <see cref="IEavAttribute.ValueKind"/> property of the attributes.<br/>
    /// </summary>
    public class EavRawValueConverterProvider : IEavRawValueConverterProvider
    {
        private readonly Dictionary<Func<IEavAttribute, bool>, IEavRawValueConverter> converters = new();

        /// <summary>
        /// Registers some "generic converters" based on the
        /// <see cref="IEavAttribute.ValueKind"/> property of the attributes.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When registering specific <see cref="IEavRawValueConverter"/> implementation,
        /// this method should be called after all 
        /// <see cref="EavRawValueConverterProvider.Register(Func{IEavAttribute, bool}, IEavRawValueConverter)"/>
        /// calls to avoid generic converters to mask other implementations:
        /// </para>
        /// <code>
        /// var provider = new EavRawValueConverterProvider();
        /// // Register specific converter for booleans
        /// provider.Register(a => a.ValueKind == EavValueKind.Boolean, new SomeImplementation());
        /// // Then register all generic converters, they will be check after the previous one
        /// provider.RegisterGenericConverters();
        /// </code>
        /// <para>
        /// The generic converters are for the following <see cref="EavValueKind"/>:
        /// </para>
        /// <list type="table">
        ///     <listheader>
        ///         <term><see cref="EavValueKind"/> from <see cref="IEavAttribute.ValueKind"/></term>
        ///         <description><see cref="IEavRawValueConverter"/> implementation</description>
        ///     </listheader>
        ///     <item>
        ///         <term><see cref="EavValueKind.Boolean"/></term>
        ///         <description>
        ///             <see cref="EavRawValueToBoolConverter"/> converts to <see cref="bool"/>
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="EavValueKind.DateTime"/></term>
        ///         <description>
        ///             <see cref="EavRawValueToDateTimeConverter"/> converts to <see cref="DateTime"/>
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="EavValueKind.Number"/></term>
        ///         <description>
        ///             <see cref="EavRawValueToDoubleConverter"/> converts to <see cref="double"/>
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="EavValueKind.Integer"/></term>
        ///         <description>
        ///             <see cref="EavRawValueToLongConverter"/> converts to <see cref="long"/>
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="EavValueKind.String"/></term>
        ///         <description>
        ///             <see cref="EavRawValueToStringConverter"/> converts to <see cref="string"/>
        ///         </description>
        ///     </item>
        /// </list>
        /// 
        /// </remarks>
        public void RegisterGenericConverters()
        {
            Register(a => a.ValueKind == EavValueKind.Boolean, new EavRawValueToBoolConverter());
            Register(a => a.ValueKind == EavValueKind.DateTime, new EavRawValueToDateTimeConverter());
            Register(a => a.ValueKind == EavValueKind.Number, new EavRawValueToDoubleConverter());
            Register(a => a.ValueKind == EavValueKind.Integer, new EavRawValueToLongConverter());
            Register(a => a.ValueKind == EavValueKind.String, new EavRawValueToStringConverter());
        }

        /// <summary>
        /// Gets the converter matching the <see cref="IEavAttribute"/>
        /// </summary>
        /// <remarks>
        /// <para>
        /// When registering specific <see cref="IEavRawValueConverter"/> implementations,
        /// the <see cref="EavRawValueConverterProvider.RegisterGenericConverters"/>
        /// method should be called after all 
        /// <see cref="EavRawValueConverterProvider.Register(Func{IEavAttribute, bool}, IEavRawValueConverter)"/>
        /// calls to avoid generic converters to mask other implementations:
        /// </para>
        /// <code>
        /// var provider = new EavRawValueConverterProvider();
        /// // Register specific converter for booleans
        /// provider.Register(a => a.ValueKind == EavValueKind.Boolean, new SomeImplementation());
        /// // Then register all generic converters, they will be check after the previous one
        /// provider.RegisterGenericConverters();
        /// </code>
        /// </remarks>
        /// <param name="attribute"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IEavRawValueConverter GetConverter(IEavAttribute attribute)
        {
            if (!TryGetConverter(attribute, out var converter) || converter == null)
                throw new Exception(string.Format("No converter registered for this attribute : {0}", attribute.AttributeName));

            return converter;
        }

        /// <summary>
        /// Registers a converter for attributes matching the validator.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When registering specific <see cref="IEavRawValueConverter"/> implementations,
        /// the <see cref="EavRawValueConverterProvider.RegisterGenericConverters"/>
        /// method should be called after all 
        /// <see cref="EavRawValueConverterProvider.Register(Func{IEavAttribute, bool}, IEavRawValueConverter)"/>
        /// calls to avoid generic converters to mask other implementations:
        /// </para>
        /// <code>
        /// var provider = new EavRawValueConverterProvider();
        /// // Register specific converter for booleans
        /// provider.Register(a => a.ValueKind == EavValueKind.Boolean, new SomeImplementation());
        /// // Then register all generic converters, they will be check after the previous one
        /// provider.RegisterGenericConverters();
        /// </code>
        /// </remarks>
        /// <param name="validator"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IEavRawValueConverterProvider Register(Func<IEavAttribute, bool> validator, IEavRawValueConverter converter)
        {
            var key = validator;

            if (converters.ContainsKey(key))
                throw new Exception("Converter already registered");

            converters.Add(key, converter);

            return this;
        }

        /// <summary>
        /// Sets the converter for the <see cref="IEavAttribute"/> to the referenced parameter
        /// <em>converted</em>. Returns false if no converter where found.
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public bool TryGetConverter(IEavAttribute attribute, out IEavRawValueConverter? converter)
        {
            foreach (var key in converters.Keys)
                if (key(attribute))
                    return (converter = converters[key]) != null;

            converter = null;
            return false;
        }
    }
}
