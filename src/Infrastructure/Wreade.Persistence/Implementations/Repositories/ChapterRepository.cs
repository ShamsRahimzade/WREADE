﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wreade.Application.Abstractions.Repostories;
using Wreade.Domain.Entities;
using Wreade.Persistence.DAL;
using Wreade.Persistence.Implementations.Repositories.Generic;

namespace Wreade.Persistence.Implementations.Repositories
{
    public class ChapterRepository:Repository<Chapter>, IChapterRepository
    {
        public ChapterRepository(AppDbContext context):base(context)
        {

        }

    }
}
