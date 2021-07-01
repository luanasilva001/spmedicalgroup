using senai.sp_medicals.webApi.ViewModel;
using System.Threading.Tasks;

namespace senai.sp_medicals.webApi.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
        Task SendWelcomeEmailAsync(WelcomeRequest request);

    }
}
