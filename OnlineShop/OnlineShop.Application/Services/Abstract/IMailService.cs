using System.Threading.Tasks;
using OnlineShop.Domain.CommonModels.Mail;

namespace OnlineShop.Application.Services.Abstract
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}