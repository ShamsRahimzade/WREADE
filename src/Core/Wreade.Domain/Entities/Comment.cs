using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wreade.Domain.Entities.Common;

namespace Wreade.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }
        public string AppUserId { get; set; }
        public AppUser Author { get; set; }
        public int? CommentedBookId { get; set; }
        public Book? CommentedBook { get; set; }
        public int? CommentedChapterId { get; set; }
        public Chapter? CommentedChapter { get; set; }

    }
}
