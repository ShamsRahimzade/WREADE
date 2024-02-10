using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wreade.Application.Abstractions.Repostories.Generic;
using Wreade.Domain.Entities;

namespace Wreade.Application.Abstractions.Repostories
{
    public interface IChapterViewCountRepository
    {
        Task<int> GetViewCountAsync(int chapterId);
        Task IncrementViewCountAsync(int chapterId);
    }
}
