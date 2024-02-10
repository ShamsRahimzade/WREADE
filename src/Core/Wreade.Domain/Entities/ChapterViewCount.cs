using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wreade.Domain.Entities
{
    public class ChapterViewCount
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Chapter")]
        public int ChapterId { get; set; }

        public int ViewCount { get; set; }

        
        public virtual Chapter Chapter { get; set; }
    }
}
