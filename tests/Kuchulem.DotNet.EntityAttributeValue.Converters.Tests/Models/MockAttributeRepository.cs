using Kuchulem.DotNet.EntityAttributeValue.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuchulem.DotNet.EntityAttributeValue.Tests.Converters.Models
{
    public class MockAttributeRepository
    {
        private readonly IEnumerable<MockAttribute> attributes = new[]
        {
            new MockAttribute
            {
                AttributeName = "string-value",
                ValueKind = EAVValueKind.String,
                ValueListKind = EAVValueListKind.None
            },
            new MockAttribute
            {
                AttributeName = "int-value",
                ValueKind = EAVValueKind.Integer,
                ValueListKind = EAVValueListKind.None
            },
            new MockAttribute
            {
                AttributeName = "double-value",
                ValueKind = EAVValueKind.Number,
                ValueListKind = EAVValueListKind.None
            },
            new MockAttribute
            {
                AttributeName = "bool-value-true",
                ValueKind = EAVValueKind.Boolean,
                ValueListKind = EAVValueListKind.None
            },
            new MockAttribute
            {
                AttributeName = "bool-value-false",
                ValueKind = EAVValueKind.Boolean,
                ValueListKind = EAVValueListKind.None
            },
            new MockAttribute
            {
                AttributeName = "datetime-value",
                ValueKind = EAVValueKind.DateTime,
                ValueListKind = EAVValueListKind.None
            },
            new MockAttribute
            {
                AttributeName = "entity-value",
                ValueKind = EAVValueKind.Entity,
                ValueListKind = EAVValueListKind.None
            },
            new MockAttribute
            {
                AttributeName = "string-list-single",
                ValueKind = EAVValueKind.String,
                ValueListKind = EAVValueListKind.Single
            },
            new MockAttribute
            {
                AttributeName = "int-list-single",
                ValueKind = EAVValueKind.Integer,
                ValueListKind = EAVValueListKind.Single
            },
            new MockAttribute
            {
                AttributeName = "double-list-single",
                ValueKind = EAVValueKind.Number,
                ValueListKind = EAVValueListKind.Single
            },
            new MockAttribute
            {
                AttributeName = "bool-list-single-true",
                ValueKind = EAVValueKind.Boolean,
                ValueListKind = EAVValueListKind.Single
            },
            new MockAttribute
            {
                AttributeName = "bool-list-single-false",
                ValueKind = EAVValueKind.Boolean,
                ValueListKind = EAVValueListKind.Single
            },
            new MockAttribute
            {
                AttributeName = "datetime-list-single",
                ValueKind = EAVValueKind.DateTime,
                ValueListKind = EAVValueListKind.Single
            },
            new MockAttribute
            {
                AttributeName = "entity-list-single",
                ValueKind = EAVValueKind.Entity,
                ValueListKind = EAVValueListKind.Single
            },
            new MockAttribute
            {
                AttributeName = "string-list-multiple",
                ValueKind = EAVValueKind.String,
                ValueListKind = EAVValueListKind.Multiple
            },
            new MockAttribute
            {
                AttributeName = "int-list-multiple",
                ValueKind = EAVValueKind.Integer,
                ValueListKind = EAVValueListKind.Multiple
            },
            new MockAttribute
            {
                AttributeName = "double-list-multiple",
                ValueKind = EAVValueKind.Number,
                ValueListKind = EAVValueListKind.Multiple
            },
            new MockAttribute
            {
                AttributeName = "bool-list-multiple",
                ValueKind = EAVValueKind.Boolean,
                ValueListKind = EAVValueListKind.Multiple
            },
            new MockAttribute
            {
                AttributeName = "datetime-list-multiple",
                ValueKind = EAVValueKind.DateTime,
                ValueListKind = EAVValueListKind.Multiple
            },
            new MockAttribute
            {
                AttributeName = "entity-list-multiple",
                ValueKind = EAVValueKind.Entity,
                ValueListKind = EAVValueListKind.Multiple
            },
        };

        public MockAttribute GetByKey(string key)
        {
            var attribute = attributes.Where(e => e.AttributeName == key).FirstOrDefault();

            if (attribute is not null)
                return attribute;

            throw new Exception($"{key} not found in stored attributes");
        }
    }
}
