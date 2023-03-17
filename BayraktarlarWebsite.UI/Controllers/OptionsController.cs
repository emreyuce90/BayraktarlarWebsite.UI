using BayraktarlarWebsite.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProgrammersBlog.Shared.Utilities.Helpers.Abstract;

namespace BayraktarlarWebsite.UI.Controllers
{
    public class OptionsController : Controller
    {
        private readonly IWritableOptions<SeoInfo> _writableOptions;
        //Config ayarlarını okuyacağımız classı ctor da IOptionsSnapshot olarak geçtik
        private readonly SeoInfo _seoInfo;
        public OptionsController(IOptionsSnapshot<SeoInfo> seoInfo, IWritableOptions<SeoInfo> writableOptions)
        {
            //Buraya direkt olarak section geliyor biz bu sectionun valuesini okuyacağız
            _seoInfo = seoInfo.Value;
            _writableOptions = writableOptions;
        }

        [Authorize]
        [HttpGet]
        public IActionResult About()
        {
            return View(_seoInfo);
        }

        [Authorize]
        [HttpPost]
        public IActionResult About(SeoInfo seoInfo)
        {
            if (ModelState.IsValid)
            {
                _writableOptions.Update(s =>
                {
                    s.SeoAuthor = seoInfo.SeoAuthor;
                    s.SeoDescription = seoInfo.SeoDescription;
                    s.MenuTitle = s.MenuTitle;
                    s.SeoTags = s.SeoTags;
                    s.Title = s.Title;
                });
                return View(seoInfo);
            }
            return View(seoInfo);
        }
    }
}
