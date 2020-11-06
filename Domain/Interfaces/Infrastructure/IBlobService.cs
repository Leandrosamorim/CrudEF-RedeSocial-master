using System.IO;
using System.Threading.Tasks;

namespace Domain.Models.Interfaces.Services
{
    public interface IBlobService
    {
        Task<string> UploadAsync(string Uri);
        Task DeleteAsync(string BlobName);
    }
}