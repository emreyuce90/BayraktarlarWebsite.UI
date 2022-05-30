using BayraktarlarWebsite.DAL.Context;
using BayraktarlarWebsite.Entities.Entities;
using BayraktarlarWebsite.UI.Helpers.Abstract;
using BayraktarlarWebsite.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.UI.Controllers
{
    public class TabelaController : Controller
    {
        private readonly DatabaseConnection _context;
        private readonly IToastNotification _toastNotification;
        private readonly IImageHelper _imageHelper;

        public TabelaController(DatabaseConnection context, IToastNotification toastNotification, IImageHelper imageHelper)
        {
            _context = context;
            _toastNotification = toastNotification;
            _imageHelper = imageHelper;
        }
        [HttpGet]
        public IActionResult Add()
        {
            //müşteri adlarını ve idsini  çek ve customer 
            var customers = _context.Customers.Select(c => new CustomerViewModel
            {
                Id = c.Id,
                Name = c.Name,

            }).ToList();


            var brands = _context.Brands.ToList();
            var materials = _context.Materials.ToList();
            return View(new Talep
            {
                CustomerViewModel = customers,
                Brands = brands,
                Materials = materials
            });


        }
        [HttpPost]
        public async Task<IActionResult> Add(Talep talep)
        {
            if (ModelState.IsValid)
            {
                var tabela = new Tabela();
                //eğer kullanıcı tabela resmi seçtiyse
                //gelen dosya adını kayıt et
                if (talep.Picture != null)
                {
                    var fileName = await _imageHelper.UploadImageAsync("tabelaImages", talep.Picture);
                    tabela.Pictures = fileName;
                }

                tabela.BrandId = talep.BrandId;
                tabela.CreatedDate = DateTime.Now;
                tabela.CustomerId = talep.CustomerId;
                tabela.MaterialId = talep.MaterialId;
                tabela.Notes = talep.Notes;
                tabela.StatusId = 1;
                tabela.UserId = 1;

                _context.Tabelas.Add(tabela);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Tabela ekleme işlemi başarılı");
                return RedirectToAction("Index");


            }
            else
            {
                var customers = _context.Customers.Select(c => new CustomerViewModel
                {
                    Id = c.Id,
                    Name = c.Name,

                }).ToList();

                var brands = _context.Brands.ToList();
                var materials = _context.Materials.ToList();
                _toastNotification.AddErrorToastMessage("Bir hata meydana geldi", new ToastrOptions
                {
                    Title = "İşlem Başarılı"
                });
                return View(new Talep
                {
                    CustomerViewModel = customers,
                    Brands = brands,
                    Materials = materials
                });
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            //databaseden tüm tabelaları çekelim
            var tabelalar = _context.Tabelas.Include(t => t.Brand).Include(t => t.Material).Include(t => t.Status).Include(t => t.Customer).Where(t => t.IsActive).ToList();

            //Liste olarak TabelaViewModel nesnesi  oluşturalım
            List<TabelaViewModel> model = new List<TabelaViewModel>();

            //databasedeki verileri gezelim ve boş olan modelimize ekleyelim
            foreach (var talep in tabelalar)
            {
                var t = new TabelaViewModel
                {
                    Id = talep.Id,
                    BrandName = talep.Brand.Name,
                    CreatedDate = talep.CreatedDate,
                    CustomerName = talep.Customer.Name,
                    MaterialName = talep.Material.Name,
                    Notes = talep.Notes,
                    SmallThumbnail = talep.Pictures,
                    StatusName = talep.Status.Name
                };
                model.Add(t);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int tabelaId)
        {
            //parametre ile gelen idye ait tabelayı bul
            var tabela = await _context.Tabelas.Include(t => t.Brand).Include(t => t.Material).Include(t => t.Customer).FirstOrDefaultAsync(t => t.Id == tabelaId);
            var model = new TabelaUpdateViewModel
            {
                Id = tabelaId,
                Brands = _context.Brands.ToList(),
                BrandId = tabela.BrandId,
                Materials = _context.Materials.ToList(),
                MaterialId = tabela.MaterialId,
                Notes = tabela.Notes,
                SmallThumbnail = tabela.Pictures,
                CustomerId = tabela.Customer.Id,
                StatusId = tabela.StatusId,
                CreatedDate = tabela.CreatedDate,
                CustomerName = tabela.Customer.Name

            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(TabelaUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tabela = await _context.Tabelas.Include(t => t.Brand).Include(t => t.Material).Include(t => t.Customer).FirstOrDefaultAsync(t => t.Id == model.Id);
                //eski görsel
                var oldPhoto = tabela.Pictures;

                if (model.Picture != null)
                {
                    var img = await _imageHelper.UploadImageAsync("tabelaImages", model.Picture);
                    tabela.Pictures = img;
                }

                if (oldPhoto != null)
                {
                    //eski foto var ise bu fotoyu sil
                    await _imageHelper.DeletePhotoAsync("tabelaImages", oldPhoto);
                }

                tabela.MaterialId = model.MaterialId;
                tabela.BrandId = model.BrandId;
                tabela.Notes = model.Notes;
                tabela.ModifiedDate = DateTime.Now;
                tabela.UserId = 1;
                tabela.CustomerId = model.CustomerId;
                tabela.StatusId = model.StatusId;
                _context.Tabelas.Update(tabela);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Tabela güncelleme işlemi başarılı", new ToastrOptions
                {
                    Title = "Güncelleme Başarılı"
                });
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int tabelaId)
        {
            //database deki veriyi çek
            var tabela = _context.Tabelas.Where(t => t.Id == tabelaId);
            //eğer veritabanında böyle bir kayıt var ise 
            if (tabela != null)
            {
                _context.Tabelas.Remove(new Tabela { Id = tabelaId });
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Tabela silme işlemi başarılı", new ToastrOptions
                {
                    Title = "İşlem Başarılı"
                });
                return NoContent();
            }
            _toastNotification.AddErrorToastMessage("Tabela silme işlemi başarısız", new ToastrOptions
            {
                Title = "İşlem Başarızı"
            });
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> ReadNote(int tabelaId)
        {
            //db den tabelaya ait notu çek
            var note = await _context.Tabelas.FirstOrDefaultAsync(a => a.Id == tabelaId);
            var model = new NoteViewModel();
            model.Note = note.Notes;
            return PartialView("ReadTabelaNotesPartialView",model);
        }
    }
}
