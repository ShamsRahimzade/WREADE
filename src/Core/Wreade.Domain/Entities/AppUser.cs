﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Wreade.Domain.Enums;

namespace Wreade.Domain.Entities
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? SelfInformation { get; set; }
		public string? Instagram { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        public DateTime Birthday { get; set; }
        public List<Book>? Books { get; set; }
        public string MainImage { get; set; } = "default.jpg";
		public string BackImage { get; set; } = "default.jpg";
        public int FollowingCount { get; set; } = 0;
        public int FollowerCount { get; set; } = 0;
        public ICollection<Follow> Followers { get; set; }
        public ICollection<Follow> Followees { get; set; }
		public bool IsPremium { get; set; }
		public List<LibraryItem>? LibraryItems { get; set; }
		public DateTime PremiumStartDate { get; set; }
		public DateTime PremiumEndDate { get; set; }
        public decimal Premiumprice { get; set; }
        public AppUser()
		{
			MainImage = "default.jpg";
			BackImage = "default.jpg";

		}
	}
}
