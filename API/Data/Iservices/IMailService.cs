using System.Threading.Tasks;
using Dtos;

namespace Data.IServices
{
    public interface IMailService
    {
        Task SendAsync(MailToSendDto mail);
    }
}