using Kuchulem.DotNet.EntityAttributeValue.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuchulem.DotNet.EntityAttributeValue.Tests.Converters.Models
{
    public class MockAttribute : IEavAttribute
    {
        public string? AttributeName { get; set; }
        public EavValueKind ValueKind { get; set; }
        public EavValueListKind ValueListKind {get; set; }
        public string? Source { get; set; }
    }
}
