
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;

namespace SGP.Utils
{
	public class EmailSender : IEmailSender
	{

		public Task SendEmailAsync(string email, string subject, string message)
		{
			MailMessage m = new MailMessage();
			SmtpClient sc = new SmtpClient();
			m.From = new MailAddress("info@sgp.lk");
			m.To.Add(email);
			m.Subject = subject;
			m.IsBodyHtml = true;
			m.Body = message;
			sc.Host = "mail5018.site4now.net";
			string str1 = "gmail.com";
			string str2 = "info@sgp.lk".ToLower();
			if (str2.Contains(str1))
			{
				try
				{
					sc.Port = 587;
					sc.Credentials = new System.Net.NetworkCredential("info@sgp.lk", "Sgp@1957");
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
					sc.Credentials = new System.Net.NetworkCredential("info@sgp.lk", "Sgp@1957");
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