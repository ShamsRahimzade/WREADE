﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Wreade.Domain.Entities
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public List<Book>? Books { get; set; }
        public List<Image>? Images { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
    }
}
