using AutoMapper;
using BayraktarlarWebsite.Entities.Entities;
using BayraktarlarWebsite.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.UI.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IToastNotification _toastNotification;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        public RolesController(RoleManager<Role> roleManager, IMapper mapper, IToastNotification toastNotification, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _toastNotification = toastNotification;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return View(new RoleListDto { Roles = roles });
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
                    _toastNotification.AddSuccessToastMessage("Rol ekleme başarılı", new ToastrOptions
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
        public async Task<IActionResult> UserWithRoles()
        {
            var userRole = new List<UserWithRolesViewModel> { };
            //Tüm kullanıcılar
            var allUsers = await _userManager.Users.ToListAsync();
            var roles = await _roleManager.Roles.ToListAsync();
            //Tüm kullanıcıları dön
            foreach (var user in allUsers)
            {
                var usr = new UserWithRolesViewModel
                {
                    Username = user.UserName,
                    EMail = user.Email,
                    UserId = user.Id,
                    Roles = new List<RoleViewModel> { }
                };
                foreach (var role in roles)
                {
                    //01 admin rolüne sahip mi
                    bool r =await _userManager.IsInRoleAsync(user, role.Name);
                    //evet ise 
                    if (r)
                    {
                        var rr = new RoleViewModel { RoleId = role.Id,RoleName = role.Name };
                        usr.Roles.Add(rr);
                    }
                }
                userRole.Add(usr);
            };
            return View(userRole);


        }

        [HttpGet]
        public IActionResult AssignRoles()
        {
            return View();
        }

    }
}

