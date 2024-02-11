using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Wreade.Domain.Entities;
using Wreade.Application.Abstractions.Services.MailService;

namespace Wreade.Persistence.Implementations.Services
{
	public class MailService:IMailService
	{
		private readonly IConfiguration _configuration;

		public MailService(IConfiguration configuration)
        {
			_configuration = configuration;
		}
		public async Task SendEmailAsync(string ToEmail, string subject, string body, bool ishtml)
		{
			SmtpClient smtpClient = new SmtpClient(_configuration["Email:Host"], Convert.ToInt32(_configuration["Email:Port"]));
			smtpClient.EnableSsl = true;
			smtpClient.Credentials = new NetworkCredential(_configuration["Email:LoginEmail"], _configuration["Email:Password"]);
			MailAddress from = new MailAddress(_configuration["Email:LoginEmail"], "Wreade");
			MailAddress to = new MailAddress(ToEmail);
			MailMessage message = new MailMessage(from, to);
			message.Subject = subject;
			message.Body = body;
			message.IsBodyHtml = ishtml;
			await smtpClient.SendMailAsync(message);
		}
	}
}
