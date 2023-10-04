using Kuchulem.DotNet.EntityAttributeValue.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuchulem.DotNet.EntityAttributeValue.Tests.Converters.Models
{
    internal class EavRawValueToMockEntityConverter : IEavRawValueConverter
    {
        private readonly MockEntityRepository entityRepository;

        public EavRawValueToMockEntityConverter(MockEntityRepository entityRepository)
        {
            this.entityRepository = entityRepository;
        }

        public object Convert(string value)
        {
            return entityRepository.GetByKey(value);
        }

        public string ConvertBack(object value)
        {
            if (value is MockEntity entity)
                return entity.Id;

            throw new Exception("Not a MockEntity");
        }
    }
}
