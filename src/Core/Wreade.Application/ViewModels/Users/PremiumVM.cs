using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wreade.Domain.Entities;

namespace Wreade.Application.ViewModels.Users
{
	public class PremiumVM
	{
		public AppUser User { get; set; }
		public DateTime PremiumStartDate { get; set; }
		public DateTime PremiumEndDate { get; set; }
		public bool IsPremium { get; set; }
        public decimal PremiumPrice { get; set; }
    }
}
