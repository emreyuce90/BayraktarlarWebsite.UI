using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.UI.Helpers.Abstract
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(string fileRoot,IFormFile picture);
    }
}
