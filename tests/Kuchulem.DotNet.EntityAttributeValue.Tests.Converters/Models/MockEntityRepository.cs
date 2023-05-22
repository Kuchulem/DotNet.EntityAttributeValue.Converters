using Kuchulem.DotNet.EntityAttributeValue.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuchulem.DotNet.EntityAttributeValue.Tests.Converters.Models
{
    internal class MockEntityRepository
    {
        private readonly MockAttributeRepository attributeRepository = new();

        private readonly IEnumerable<MockEntity> entities;

        public MockEntityRepository()
        {
            entities = new[]
            {
                new MockEntity
                {
                    Id = "valid-entity",
                    Values = new[]
                    {
                        new MockValue
                        {
                            Attribute = attributeRepository.GetByKey("string-value"),
                            RawValue = "lorem ipsum"
                        },
                        new MockValue
                        {
                            Attribute = attributeRepository.GetByKey("int-value"),
                            RawValue = 2.ToString(CultureInfo.InvariantCulture)
                        },
                        new MockValue
                        {
                            Attribute = attributeRepository.GetByKey("double-value"),
                            RawValue = 3.1415.ToString(CultureInfo.InvariantCulture)
                        },
                        new MockValue
                        {
                            Attribute = attributeRepository.GetByKey("bool-value-true"),
                            RawValue = true.ToString(CultureInfo.InvariantCulture)
                        },
                        new MockValue
                        {
                            Attribute = attributeRepository.GetByKey("bool-value-false"),
                            RawValue = false.ToString(CultureInfo.InvariantCulture)
                        },
                        new MockValue
                        {
                            Attribute = attributeRepository.GetByKey("datetime-value"),
                            RawValue = new DateTime(2023, 5, 15, 19, 4, 0).ToString(CultureInfo.InvariantCulture)
                        },
                        new MockValue
                        {
                            Attribute = attributeRepository.GetByKey("entity-value"),
                            RawValue = "child"
                        },
                        new MockValue
                        {
                            Attribute = attributeRepository.GetByKey("string-list-single"),
                            RawValue = "lorem ipsum"
                        },
                        new MockValue
                        {
                            Attribute = attributeRepository.GetByKey("int-list-single"),
                            RawValue = 2.ToString(CultureInfo.InvariantCulture)
                        },
                        new MockValue
                        {
                            Attribute = attributeRepository.GetByKey("double-list-single"),
                            RawValue = 3.1415.ToString(CultureInfo.InvariantCulture)
                        },
                        new MockValue
                        {
                            Attribute = attributeRepository.GetByKey("bool-list-single-true"),
                            RawValue = true.ToString(CultureInfo.InvariantCulture)
                        },
                        new MockValue
                        {
                            Attribute = attributeRepository.GetByKey("bool-list-single-false"),
                            RawValue = false.ToString(CultureInfo.InvariantCulture)
                        },
                        new MockValue
                        {
                            Attribute = attributeRepository.GetByKey("datetime-list-single"),
                            RawValue = new DateTime(2023, 5, 15, 19, 4, 0).ToString(CultureInfo.InvariantCulture)
                        },
                        new MockValue
                        {
                            Attribute = attributeRepository.GetByKey("entity-list-single"),
                            RawValue = "child"
                        },
                        new MockValue
                        {
                            Attribute = attributeRepository.GetByKey("string-list-multiple"),
                            RawValue = JsonConvert.SerializeObject(new[]{"lorem", "ipsum" })
                        },
                        new MockValue
                        {
                            Attribute = attributeRepository.GetByKey("int-list-multiple"),
                            RawValue = JsonConvert.SerializeObject(new[]{ 2.ToString(CultureInfo.InvariantCulture) , 3.ToString(CultureInfo.InvariantCulture) })
                        },
                        new MockValue
                        {
                            Attribute = attributeRepository.GetByKey("double-list-multiple"),
                            RawValue = JsonConvert.SerializeObject(new[]{ 3.1415.ToString(CultureInfo.InvariantCulture) , 2.54.ToString(CultureInfo.InvariantCulture) })
                        },
                        new MockValue
                        {
                            Attribute = attributeRepository.GetByKey("bool-list-multiple"),
                            RawValue = JsonConvert.SerializeObject(new[]{ true.ToString(CultureInfo.InvariantCulture) , false.ToString(CultureInfo.InvariantCulture) })
                        },
                        new MockValue
                        {
                            Attribute = attributeRepository.GetByKey("datetime-list-multiple"),
                            RawValue = JsonConvert.SerializeObject(new[]{ new DateTime(2023, 5, 15, 19, 4, 0).ToString(CultureInfo.InvariantCulture) , new DateTime(2023, 5, 19, 14, 24, 0).ToString(CultureInfo.InvariantCulture) })
                        },
                        new MockValue
                        {
                            Attribute = attributeRepository.GetByKey("entity-list-multiple"),
                            RawValue = JsonConvert.SerializeObject(new[]{ "child", "other-child" })
                        },
                    }
                },
                new MockEntity
                {
                    Id = "child",
                    Values = new[]
                    {
                        new MockValue
                        {
                            Attribute = attributeRepository.GetByKey("string-value"),
                            RawValue = "lorem ipsum"
                        },
                    }
                },
                new MockEntity
                {
                    Id = "other-child",
                    Values = new[]
                    {
                        new MockValue
                        {
                            Attribute = attributeRepository.GetByKey("string-value"),
                            RawValue = "lorem ipsum"
                        },
                    }
                }
            };
        }



        public MockEntity GetByKey(string key)
        {
            var entity = entities.Where(e => e.Id == key).FirstOrDefault();
            if (entity is not null)
                return entity;

            throw new Exception($"{key} not found in stored entities");
        }
    }
}
