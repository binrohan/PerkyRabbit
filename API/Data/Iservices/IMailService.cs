using System.Threading.Tasks;
using API.Dtos;

namespace Data.IServices
{
    public interface IMailService
    {
         Task SendAsync(MailToSend mail);
    }
}