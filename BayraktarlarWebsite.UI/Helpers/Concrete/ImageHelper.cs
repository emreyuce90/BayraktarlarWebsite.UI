using BayraktarlarWebsite.UI.Helpers.Abstract;
using BayraktarlarWebsite.UI.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.UI.Helpers.Concrete
{
    public class ImageHelper : IImageHelper
    {
        private readonly IWebHostEnvironment _env;

        public ImageHelper(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> UploadImageAsync(string fileRoot,IFormFile picture)
        {
            //~wwwroot uzantısı
            string wwwroot = _env.WebRootPath;
            //dosya adı
            string fileName = Path.GetFileNameWithoutExtension(picture.FileName);
            //uzantısı
            string fileExtension = Path.GetExtension(picture.FileName);
            //tarih
            DateTime dateTime = DateTime.Now;
            //emreyüce_260520022.png
            string dosyaAdi = $"{fileName}_{DateTimeGenerator.GetDateTimeWithUnderScore(dateTime)}_{fileExtension}";

            //wwwroot/img/tabelaImages/emreyüce_260520022.png
            var path = Path.Combine($"{wwwroot}/img/{fileRoot}",dosyaAdi);
            await using (var stream = new FileStream(path,FileMode.Create))
            {
                await picture.CopyToAsync(stream);
            }
            return dosyaAdi;
        }
    }
}
