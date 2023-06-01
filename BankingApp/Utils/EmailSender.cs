
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;

namespace BankingApp.Utils
{
	public class EmailSender : IEmailSender
	{
		private readonly string _server;
		private readonly string _user;
		private readonly string _pass;

		public EmailSender(string smptServer, string smtpUser, string smptPassword)
		{
			_server = smptServer;
			_user = smtpUser;
			_pass = smptPassword;
		}

		public Task SendEmailAsync(string email, string subject, string message)
		{
			MailMessage m = new MailMessage();
			SmtpClient sc = new SmtpClient();
			m.From = new MailAddress(_user);
			m.To.Add(email);
			m.Subject = subject;
			m.IsBodyHtml = true;
			m.Body = message;
			sc.Host = _server;
			try
			{
				sc.Port = 587;
				sc.Credentials = new System.Net.NetworkCredential(_user, _pass);
				sc.EnableSsl = true;
				return sc.SendMailAsync(m);

			}
			catch (Exception e1)
			{
				throw new Exception(e1.Message);
			}

		}
	}
}