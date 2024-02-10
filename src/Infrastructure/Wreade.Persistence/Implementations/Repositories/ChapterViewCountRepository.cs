using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wreade.Application.Abstractions.Repostories;
using Wreade.Domain.Entities;
using Wreade.Persistence.DAL;

namespace Wreade.Persistence.Implementations.Repositories
{
    public class ChapterViewCountRepository : IChapterViewCountRepository
    {
        private readonly AppDbContext _context;

        public ChapterViewCountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetViewCountAsync(int chapterId)
        {
            var viewCount = await _context.ChapterViewCounts
                .Where(c => c.ChapterId == chapterId)
                .Select(c => c.ViewCount)
                .FirstOrDefaultAsync();

            return viewCount;
        }

        public async Task IncrementViewCountAsync(int chapterId)
        {
            var chapterViewCount = await _context.ChapterViewCounts
                .Where(c => c.ChapterId == chapterId)
                .FirstOrDefaultAsync();

            if (chapterViewCount == null)
            {
                chapterViewCount = new ChapterViewCount
                {
                    ChapterId = chapterId,
                    ViewCount = 1
                };
                _context.ChapterViewCounts.Add(chapterViewCount);
            }
            else
            {
                chapterViewCount.ViewCount++;
            }

            await _context.SaveChangesAsync();

        }
    }
}
