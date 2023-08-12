using UmbracoWebApp.Models.SendEmail.Dtos;

namespace UmbracoWebApp.Interfaces.SendEmail
{
    public interface ISendEmail
    {
        Task SendEmailAsync(string toEmail, string subject, string body);

        Task<bool> SendEmailUmbraco(EmailRequestDto model);
    }
}
