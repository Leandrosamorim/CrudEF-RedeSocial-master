using System.Threading.Tasks;

namespace Domain.Models.Interfaces.Services
{
    public interface IQueueMessage
    {
        Task SendAsync(string messageText);
    }
}