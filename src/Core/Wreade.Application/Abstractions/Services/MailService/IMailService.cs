using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wreade.Application.ViewModels.MailSender;

namespace Wreade.Application.Abstractions.Services.MailService
{
	public interface IMailService
	{
		Task SendEmailAsync(string ToEmail, string subject, string body, bool ishtml);
	}
}
