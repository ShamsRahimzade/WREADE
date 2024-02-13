using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wreade.Domain.Entities.Common;

namespace Wreade.Domain.Entities
{
    public class Chapter:BaseEntity
    {
        public string Title { get; set; }
        public string Text { get; set; }
        [NotMapped]
        public string ChapterImage { get; set; }
		[ForeignKey("BookId")]
		public int BookId { get; set; }
        public Book Book { get; set; }
        public ICollection<Comment>? Comments { get; set; }
		public virtual ChapterViewCount ChapterViewCount { get; set; }
       
    }
}
