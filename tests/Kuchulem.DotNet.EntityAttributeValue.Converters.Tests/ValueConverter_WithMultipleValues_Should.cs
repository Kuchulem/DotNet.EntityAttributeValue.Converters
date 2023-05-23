using Kuchulem.DotNet.EntityAttributeValue.Converters;
using Kuchulem.DotNet.EntityAttributeValue.Tests.Converters.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Kuchulem.DotNet.EntityAttributeValue.Converters.Tests
{
    public class ValueConverter_WithMultipleValues_Should
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
            valueConverterProvider.Register(attributesRepository.GetByKey("entity-list-multiple"), new EAVValueToMockEntityConverter(entityRepository));
        }

        [Test]
        public void ConvertToStrings()
        {
            var entity = entityRepository.GetByKey("valid-entity");

            var valueConverter = new ValueConverter(valueConverterProvider);

            var value = valueConverter.ConvertList<string, MockValue, MockEntity, MockAttribute>(entity.Values.Where(v => v.Attribute?.AttributeName == "string-list-multiple").First());

            Assert.That(value, Is.EqualTo(new[] { "lorem", "ipsum" }));
        }

        [Test]
        public void ConvertToIntegers()
        {
            var entity = entityRepository.GetByKey("valid-entity");

            var valueConverter = new ValueConverter(valueConverterProvider);

            var value = valueConverter.ConvertList<int, MockValue, MockEntity, MockAttribute>(entity.Values.Where(v => v.Attribute?.AttributeName == "int-list-multiple").First());

            Assert.That(value, Is.EqualTo(new[] { 2, 3 }));
        }

        [Test]
        public void ConvertToDoubles()
        {
            var entity = entityRepository.GetByKey("valid-entity");

            var valueConverter = new ValueConverter(valueConverterProvider);

            var value = valueConverter.ConvertList<double, MockValue, MockEntity, MockAttribute>(entity.Values.Where(v => v.Attribute?.AttributeName == "double-list-multiple").First());

            Assert.That(value, Is.EqualTo(new[] { 3.1415, 2.54 }));
        }

        [Test]
        public void ConvertToBooleans()
        {
            var entity = entityRepository.GetByKey("valid-entity");

            var valueConverter = new ValueConverter(valueConverterProvider);

            var value = valueConverter.ConvertList<bool, MockValue, MockEntity, MockAttribute>(entity.Values.Where(v => v.Attribute?.AttributeName == "bool-list-multiple").First());

            Assert.That(value, Is.EqualTo(new[] { true, false }));
        }

        [Test]
        public void ConvertToDateTimes()
        {
            var entity = entityRepository.GetByKey("valid-entity");

            var valueConverter = new ValueConverter(valueConverterProvider);

            var value = valueConverter.ConvertList<DateTime, MockValue, MockEntity, MockAttribute>(entity.Values.Where(v => v.Attribute?.AttributeName == "datetime-list-multiple").First());

            Assert.That(value, Is.EqualTo(new[] { new DateTime(2023, 5, 15, 19, 4, 0), new DateTime(2023, 5, 19, 14, 24, 0) }));
        }

        [Test]
        public void ConvertToEntities()
        {
            var entity = entityRepository.GetByKey("valid-entity");

            var valueConverter = new ValueConverter(valueConverterProvider);

            var value = valueConverter.ConvertList<MockEntity, MockValue, MockEntity, MockAttribute>(entity.Values.Where(v => v.Attribute?.AttributeName == "entity-list-multiple").First());

            Assert.That(value, Is.EqualTo(new[] { entityRepository.GetByKey("child"), entityRepository.GetByKey("other-child") }));
        }
    }
}
