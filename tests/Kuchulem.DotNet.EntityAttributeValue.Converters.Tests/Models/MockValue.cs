using Kuchulem.DotNet.EntityAttributeValue.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuchulem.DotNet.EntityAttributeValue.Tests.Converters.Models
{
    public class MockValue : IEAVValue<MockEntity, MockAttribute>
    {
        public string? RawValue { get; set; } = null;
        public MockAttribute? Attribute { get; set; }
        public MockEntity? Entity { get; set; }
    }
}
