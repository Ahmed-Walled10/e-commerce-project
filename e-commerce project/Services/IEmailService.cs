
namespace e_commerce_project.Services
{
    public interface IEmailService
    {

        Task SendEmailAsync(string to, string subject, string body, bool isBodyHtml = true);

    }
}
