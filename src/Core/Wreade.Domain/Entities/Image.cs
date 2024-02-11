using Wreade.Domain.Entities.Common;

namespace Wreade.Domain.Entities
{
    public class Image:BaseEntity
    {
        public string URL { get; set; }
        public bool? IsPrimary { get; set; }

        public List<ChapterImage>? ChapterImages { get; set; }
        public string AppUSerId { get; set; }
        public AppUser User { get; set; }
    }
}
