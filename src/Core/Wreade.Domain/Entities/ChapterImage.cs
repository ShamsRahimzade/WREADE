using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wreade.Domain.Entities
{
    public class ChapterImage
    {
        public int? ImageId { get; set; }
        public Image? Image { get; set; }
        public int? ChapterId { get; set; }
        public Chapter? Chapter { get; set; }
    }
}
