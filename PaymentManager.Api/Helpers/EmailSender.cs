using System;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using Serilog;

namespace PaymentManager.Api.Helpers
{
    public class EmailSender
    {
        public static async void SendEmail(string subject, string text, string toEmail)
        {
            var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("tranda96srk@gmail.com", "??????")
            };

            try
            {
                using (var message = new MailMessage("tranda96srk@gmail.com", toEmail)
                {
                    Subject = subject,
                    Body = text
                })
                {
                    try
                    {
                        await smtpClient.SendMailAsync(message);
                    }
                    catch (Exception e)
                    {
                        Log.Error($"Error while sending mail to client {toEmail}: {e.Message}");
                        Trace.WriteLine($"Error while sending mail: {e.Message}");
                    }
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
        }
    }
}