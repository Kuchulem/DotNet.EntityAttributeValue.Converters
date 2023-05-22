using Kuchulem.DotNet.EntityAttributeValue.Converters;
using Kuchulem.DotNet.EntityAttributeValue.Tests.Converters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuchulem.DotNet.EntityAttributeValue.Tests.Converters
{
    public class ValueConverter_WithList_SingleSelection_Should
    {
        private MockEntityRepository entityRepository;
        private MockAttributeRepository attributesRepository;
        private EAVValueConverterProvider valueConverterProvider;

        [SetUp]
        public void Setup()
        {
            entityRepository = new MockEntityRepository();
            attributesRepository = new MockAttributeRepository();

            valueConverterProvider = new EAVValueConverterProvider();
            valueConverterProvider.RegisterGenericConverters();
            valueConverterProvider.Register(attributesRepository.GetByKey("entity-value"), new EAVValueToMockEntityConverter(entityRepository));
            valueConverterProvider.Register(attributesRepository.GetByKey("entity-list-single"), new EAVValueToMockEntityConverter(entityRepository));
        }

        [Test]
        public void ConvertToString()
        {
            var entity = entityRepository.GetByKey("valid-entity");

            var valueConverter = new ValueConverter(valueConverterProvider);

            var value = valueConverter.Convert<string>(entity.Values.Where(v => v.Attribute?.AttributeName == "string-list-single").First());

            Assert.That(value is string strValue && strValue == "lorem ipsum", Is.True);
        }

        [Test]
        public void ConvertToInt()
        {
            var entity = entityRepository.GetByKey("valid-entity");

            var valueConverter = new ValueConverter(valueConverterProvider);

            var value = valueConverter.Convert<int>(entity.Values.Where(v => v.Attribute?.AttributeName == "int-list-single").First());

            Assert.That(value is int intValue && intValue == 2, Is.True);
        }

        [Test]
        public void ConvertToDouble()
        {
            var entity = entityRepository.GetByKey("valid-entity");

            var valueConverter = new ValueConverter(valueConverterProvider);

            var value = valueConverter.Convert<double>(entity.Values.Where(v => v.Attribute?.AttributeName == "double-list-single").First());

            Assert.That(value is double doubleValue && doubleValue == 3.1415, Is.True);
        }

        [Test]
        public void ConvertToBoolean_True()
        {
            var entity = entityRepository.GetByKey("valid-entity");

            var valueConverter = new ValueConverter(valueConverterProvider);

            var value = valueConverter.Convert<bool>(entity.Values.Where(v => v.Attribute?.AttributeName == "bool-list-single-true").First());

            Assert.That(value is bool boolValue && boolValue, Is.True);
        }

        [Test]
        public void ConvertToBoolean_False()
        {
            var entity = entityRepository.GetByKey("valid-entity");

            var valueConverter = new ValueConverter(valueConverterProvider);

            var value = valueConverter.Convert<bool>(entity.Values.Where(v => v.Attribute?.AttributeName == "bool-list-single-false").First());

            Assert.That(value is bool boolValue && !boolValue, Is.True);
        }

        [Test]
        public void ConvertToDateTime()
        {
            var entity = entityRepository.GetByKey("valid-entity");

            var valueConverter = new ValueConverter(valueConverterProvider);

            var value = valueConverter.Convert<DateTime>(entity.Values.Where(v => v.Attribute?.AttributeName == "datetime-list-single").First());

            Assert.That(value is DateTime boolValue && boolValue == new DateTime(2023, 5, 15, 19, 4, 0), Is.True);
        }

        [Test]
        public void ConvertToEntity()
        {
            var entity = entityRepository.GetByKey("valid-entity");

            var valueConverter = new ValueConverter(valueConverterProvider);

            var value = valueConverter.Convert<MockEntity>(entity.Values.Where(v => v.Attribute?.AttributeName == "entity-list-single").First());

            Assert.That(value is MockEntity child && child.Id == "child", Is.True);
        }
    }
}
