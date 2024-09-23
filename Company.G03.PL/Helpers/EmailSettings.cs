using Company.G03.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Company.G03.PL.Helpers
{
	public static class EmailSettings
	{
		public static void SendEmail (EmailFormat email)
		{
			// Mail Server : gmail
			// smtp
			var client = new SmtpClient("smtp.gmail.com", 587); 
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("adelhossam1999@gmail.com", "dphhmqewfmwgvpys");// This Pass Gmail generate it for u 
			client.Send("adelhossam1999@gmail.com", email.To, email.Subject, email.Body);



		}
	}
}
