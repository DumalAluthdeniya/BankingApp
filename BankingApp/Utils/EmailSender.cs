
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;

namespace SGP.Utils
{
	public class EmailSender : IEmailSender
	{
		private readonly IConfiguration _configuration;
		public EmailSender(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public Task SendEmailAsync(string email, string subject, string message)
		{
			MailMessage m = new MailMessage();
			SmtpClient sc = new SmtpClient();
			m.From = new MailAddress(_configuration.GetValue<string>("SMTPUser"));
			m.To.Add(email);
			m.Subject = subject;
			m.IsBodyHtml = true;
			m.Body = message;
			sc.Host = _configuration.GetValue<string>("SMTPServer");
			string str1 = "gmail.com";
			string str2 = _configuration.GetValue<string>("SMTPUser").ToLower();
			if (str2.Contains(str1))
			{
				try
				{
					sc.Port = 587;
					sc.Credentials = new System.Net.NetworkCredential(_configuration.GetValue<string>("SMTPUser"), _configuration.GetValue<string>("SMTPPassword"));
					sc.EnableSsl = true;
					return sc.SendMailAsync(m);

				}
				catch (Exception e1)
				{
					throw new Exception(e1.Message);
				}
			}
			else
			{
				try
				{
					sc.Port = 25;
					sc.Credentials = new System.Net.NetworkCredential(_configuration.GetValue<string>("SMTPUser"), _configuration.GetValue<string>("SMTPPassword"));
					sc.EnableSsl = false;
					return sc.SendMailAsync(m);
				}
				catch (Exception e2)
				{
					throw new Exception(e2.Message);
				}
			}
		}
	}
}