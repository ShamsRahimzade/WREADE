﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wreade.Domain.Entities;

namespace Wreade.Application.ViewModels
{
	public  class ChapterDetailVM
	{
        public List<Chapter> Chapters { get; set; }
        public Chapter chapter { get; set; }
    }
}
