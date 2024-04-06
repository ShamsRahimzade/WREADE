using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wreade.Application.Abstractions.Services
{
    public interface ICookieService
    {
        int GetLastReadChapter();
        void SetLastReadChapter(int chapterId);
    }
}
