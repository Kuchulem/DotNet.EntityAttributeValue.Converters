using Kuchulem.DotNet.EntityAttributeValue.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuchulem.DotNet.EntityAttributeValue.Tests.Converters.Models
{
    internal class EAVValueToMockEntityConverter : IEAVValueConverter<MockEntity>
    {
        private readonly MockEntityRepository entityRepository;

        public EAVValueToMockEntityConverter(MockEntityRepository entityRepository)
        {
            this.entityRepository = entityRepository;
        }

        public MockEntity Convert(string value)
        {
            return entityRepository.GetByKey(value);
        }

        public string ConvertBack(MockEntity value)
        {
            return value.Id;
        }
    }
}
