using Kuchulem.DotNet.EntityAttributeValue.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuchulem.DotNet.EntityAttributeValue.Tests.Converters.Models
{
    public class MockValue : IEavValue
    {
        public string? RawValue { get; set; } = null;
        public MockAttribute? Attribute { get; set; }
        public MockEntity? Entity { get; set; }

        public IEavAttribute GetEavAttribute()
            => Attribute ?? throw new Exception("No attribute");

        public object GetEntity()
        {
            return Entity ?? throw new Exception("No entity");
        }
    }
}
