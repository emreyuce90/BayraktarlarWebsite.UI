using BayraktarlarWebsite.DAL.Context;
using BayraktarlarWebsite.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BayraktarlarWebsite.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseConnection _context;

        public HomeController(DatabaseConnection context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new HomePageViewModel
            {
                OnaydaBekleyenler = _context.Tabelas.Where(t=>t.StatusId ==1).Count(),
                Onaylananlar= _context.Tabelas.Where(t => t.StatusId == 2).Count(),
                ReddedilenTabelalar = _context.Tabelas.Where(t => t.StatusId == 6).Count(),
                UygulananTabelalar = _context.Tabelas.Where(t => t.StatusId == 5).Count()
            };
            return View(model);
        }
    }
}
