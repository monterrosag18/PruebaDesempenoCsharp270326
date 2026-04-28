using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using System;

namespace SportsComplex.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void Send(string to, string subject, string body)
        {
            try
            {
                var emailMessage = new MimeMessage();
                
                // Usamos ?? "" para asegurar que nunca se pase un nulo
                var senderName = _config["SmtpSettings:SenderName"] ?? "Sports Complex";
                var senderEmail = _config["SmtpSettings:SenderEmail"] ?? "";
                
                emailMessage.From.Add(new MailboxAddress(senderName, senderEmail));
                emailMessage.To.Add(new MailboxAddress("", to));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { 
                    Text = body 
                };

                using (var client = new SmtpClient())
                {
                    // Extraemos los valores con seguridad
                    var server = _config["SmtpSettings:Server"] ?? "";
                    var portStr = _config["SmtpSettings:Port"];
                    int port = !string.IsNullOrEmpty(portStr) ? int.Parse(portStr) : 587;
                    var username = _config["SmtpSettings:Username"] ?? "";
                    var password = _config["SmtpSettings:Password"] ?? "";

                    // Validar que los datos críticos no estén vacíos antes de conectar
                    if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(username))
                    {
                        Console.WriteLine("Error: Configuración SMTP incompleta en appsettings.json");
                        return;
                    }

                    client.Connect(server, port, SecureSocketOptions.StartTls);
                    client.Authenticate(username, password);
                    client.Send(emailMessage);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error enviando correo: {ex.Message}");
            }
        }
    }
}