using AutoMapper;
using BayraktarlarWebsite.Entities.Entities;
using BayraktarlarWebsite.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Linq;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.UI.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        public RolesController(RoleManager<Role> roleManager, IMapper mapper, IToastNotification toastNotification)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _toastNotification = toastNotification;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return View(new RoleListDto { Roles = roles});
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new RoleAddViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> Add(RoleAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new Role
                {
                    Name = model.RoleName
                };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    _toastNotification.AddSuccessToastMessage("Rol ekleme başarılı",new ToastrOptions
                    {
                        Title = "İşlem Başarılı"
                    });
                    return RedirectToAction("Index");
                }
            }
            _toastNotification.AddErrorToastMessage("Rol ekleme işleminde hata oluştu", new ToastrOptions
            {
                Title = "Hata"
            });
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AssignRole()
        {
            return View();
        }
    }
}
