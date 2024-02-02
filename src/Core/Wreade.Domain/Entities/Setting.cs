using Wreade.Domain.Entities.Common;

namespace Wreade.Domain.Entities
{
    public class Setting:BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
