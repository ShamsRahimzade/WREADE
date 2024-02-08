using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wreade.Domain.Entities.Common;

namespace Wreade.Domain.Entities
{
    public class Follow : BaseEntity
    {
        public string FollowerId { get; set; }
        public string FolloweeId { get; set; }
        public AppUser Follower { get; set; }
        public AppUser Followee { get; set; }
    }
}
