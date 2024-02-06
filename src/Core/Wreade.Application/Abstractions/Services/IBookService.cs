using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Wreade.Application.ViewModels;

using Wreade.Domain.Entities;

namespace Wreade.Application.Abstractions.Services
{
	public interface IBookService
	{
		List<Book> GetReadingHistoryForUser(string userName);
       
    }
}
