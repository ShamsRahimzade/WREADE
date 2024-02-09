﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wreade.Application.ViewModels
{
    public class PaginationVM<T> where T:class,new()
    {
        public ICollection<T> Items { get; set; }
        public double TotalPage { get; set; }
        public int CurrentPage { get; set; }
    }
}