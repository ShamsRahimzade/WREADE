using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wreade.Domain.Entities.Common;

namespace Wreade.Domain.Entities
{
	public class LibraryItem:BaseEntity
	{

       
        //Relation properties
        public int BookId { get; set; }
		public Book book { get; set; }
		public string AppUserId { get; set; }
		public AppUser User { get; set; }
		
	}
}
