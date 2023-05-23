using Kuchulem.DotNet.EntityAttributeValue.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuchulem.DotNet.EntityAttributeValue.Tests.Converters.Models
{
    public class MockEntity
    {
        public string Id { get; set; } = string.Empty;

        public IEnumerable<MockValue> Values { get; set; } = Enumerable.Empty<MockValue>();
    }
}
