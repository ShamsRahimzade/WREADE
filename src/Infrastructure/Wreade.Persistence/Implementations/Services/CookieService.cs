using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Wreade.Application.Abstractions.Services;

namespace Wreade.Persistence.Implementations.Services
{
    public class CookieService:ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetLastReadChapter()
        {
            var chapterIdString = _httpContextAccessor.HttpContext.Request.Cookies["LastReadChapter"];
            if (int.TryParse(chapterIdString, out int chapterId))
            {
                return chapterId;
            }
            return 0;
        }

        public void SetLastReadChapter(int chapterId)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append("LastReadChapter", chapterId.ToString(), new CookieOptions
            {
                Expires = DateTime.UtcNow.AddMonths(3)
            });
        }
    }
}
