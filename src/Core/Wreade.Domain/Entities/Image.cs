﻿using Wreade.Domain.Entities.Common;

namespace Wreade.Domain.Entities
{
    public class Image:BaseEntity
    {
        public string URL { get; set; }
        public bool? IsPrimary { get; set; }
      
        public int BookId { get; set; }
        public Book Book { get; set; }
        public string? AppUSerId { get; set; }
        public AppUser? User { get; set; }
    }
}
