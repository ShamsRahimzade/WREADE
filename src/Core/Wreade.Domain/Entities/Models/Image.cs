﻿using Wreade.Domain.Entities.Common;

namespace WEB.Models
{
    public class Image:BaseEntity
    {
        public string URL { get; set; }
        public bool? IsPrimary { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
