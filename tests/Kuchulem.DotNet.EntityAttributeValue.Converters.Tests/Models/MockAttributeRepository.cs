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
                ValueKind = EavValueKind.String,
                ValueListKind = EavValueListKind.None
            },
            new MockAttribute
            {
                AttributeName = "int-value",
                ValueKind = EavValueKind.Integer,
                ValueListKind = EavValueListKind.None
            },
            new MockAttribute
            {
                AttributeName = "double-value",
                ValueKind = EavValueKind.Number,
                ValueListKind = EavValueListKind.None
            },
            new MockAttribute
            {
                AttributeName = "bool-value-true",
                ValueKind = EavValueKind.Boolean,
                ValueListKind = EavValueListKind.None
            },
            new MockAttribute
            {
                AttributeName = "bool-value-false",
                ValueKind = EavValueKind.Boolean,
                ValueListKind = EavValueListKind.None
            },
            new MockAttribute
            {
                AttributeName = "datetime-value",
                ValueKind = EavValueKind.DateTime,
                ValueListKind = EavValueListKind.None
            },
            new MockAttribute
            {
                AttributeName = "entity-value",
                ValueKind = EavValueKind.Entity,
                ValueListKind = EavValueListKind.None
            },
            new MockAttribute
            {
                AttributeName = "string-list-single",
                ValueKind = EavValueKind.String,
                ValueListKind = EavValueListKind.Single
            },
            new MockAttribute
            {
                AttributeName = "int-list-single",
                ValueKind = EavValueKind.Integer,
                ValueListKind = EavValueListKind.Single
            },
            new MockAttribute
            {
                AttributeName = "double-list-single",
                ValueKind = EavValueKind.Number,
                ValueListKind = EavValueListKind.Single
            },
            new MockAttribute
            {
                AttributeName = "bool-list-single-true",
                ValueKind = EavValueKind.Boolean,
                ValueListKind = EavValueListKind.Single
            },
            new MockAttribute
            {
                AttributeName = "bool-list-single-false",
                ValueKind = EavValueKind.Boolean,
                ValueListKind = EavValueListKind.Single
            },
            new MockAttribute
            {
                AttributeName = "datetime-list-single",
                ValueKind = EavValueKind.DateTime,
                ValueListKind = EavValueListKind.Single
            },
            new MockAttribute
            {
                AttributeName = "entity-list-single",
                ValueKind = EavValueKind.Entity,
                ValueListKind = EavValueListKind.Single
            },
            new MockAttribute
            {
                AttributeName = "string-list-multiple",
                ValueKind = EavValueKind.String,
                ValueListKind = EavValueListKind.Multiple
            },
            new MockAttribute
            {
                AttributeName = "int-list-multiple",
                ValueKind = EavValueKind.Integer,
                ValueListKind = EavValueListKind.Multiple
            },
            new MockAttribute
            {
                AttributeName = "double-list-multiple",
                ValueKind = EavValueKind.Number,
                ValueListKind = EavValueListKind.Multiple
            },
            new MockAttribute
            {
                AttributeName = "bool-list-multiple",
                ValueKind = EavValueKind.Boolean,
                ValueListKind = EavValueListKind.Multiple
            },
            new MockAttribute
            {
                AttributeName = "datetime-list-multiple",
                ValueKind = EavValueKind.DateTime,
                ValueListKind = EavValueListKind.Multiple
            },
            new MockAttribute
            {
                AttributeName = "entity-list-multiple",
                ValueKind = EavValueKind.Entity,
                ValueListKind = EavValueListKind.Multiple
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
