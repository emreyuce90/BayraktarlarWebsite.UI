using BayraktarlarWebsite.BLL.Interfaces;
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
        private readonly IBrandService _brandService;
        private readonly IToastNotification _toastNotification;
        private readonly IImageHelper _imageHelper;
        private readonly ICustomerService _customerService;

        public TabelaController(IToastNotification toastNotification, IImageHelper imageHelper, ICustomerService customerService,IBrandService brandService)
        {
           
            _toastNotification = toastNotification;
            _imageHelper = imageHelper;
            _customerService = customerService;
            _brandService = brandService;
        }
        [HttpGet]
        public IActionResult Add()
        {
            //müşteri datası
            //müşteri adlarını ve idsini  çek ve customer 

            var customers = _customerService.GetAllAsync();
            //markalar
            var brands = _brandService.GetAllAsync(); 
            //malzemeler
            var materials = _context.Materials.ToList();
            //modelleri dolu olarak view a return eder
            return View(new TabelaAddViewModel
            {
                TabelaCreateViewModel = new TabelaCreateViewModel
                {
                    CustomerViewModel = customers,
                    Brands = brands,
                    Materials = materials
                }
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(TabelaAddViewModel model)
        {
            //Eğer seçilen bilgiler doğru ise
            if (ModelState.IsValid)
            {
                //Yeni bir tabela nesnesi oluştur
                var table = new Tabela
                {
                    BrandId = model.TabelaCreateViewModel.BrandId,
                    CreatedDate = DateTime.Now,
                    CustomerId = model.TabelaCreateViewModel.CustomerId,
                    MaterialId = model.TabelaCreateViewModel.MaterialId,
                    Notes = model.TabelaCreateViewModel.Notes,
                    StatusId = 1,
                    UserId = 1,
                    //Tabelanın görsellerini boş nesne olarak oluştur
                    Images = new List<TabelaImages> { }

                };

                //Eğer resim seçildiyse
                if (model.TabelaImages != null)
                {
                    string fileName;
                    //resimleri dön
                    foreach (var image in model.TabelaImages)
                    {
                        //fotoğrafı upload et
                        fileName = await _imageHelper.UploadImageAsync("tabelaImages", image);
                        //oluşturduğun tabela nesnesine bu fotoğrafları ekle
                        table.Images.Add(new TabelaImages { PictureUrl = fileName });
                    }
                    //fotoğraflar bittiğinde tabela nesnesini db ye ekle
                    await _context.Tabelas.AddAsync(table);
                    await _context.SaveChangesAsync();
                }

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
                return View(new TabelaCreateViewModel
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
            var tabelalar = _context.Tabelas.Include(t => t.Brand).Include(t => t.Material).Include(t => t.Status).Include(t => t.Customer).Include(t => t.Images).Include(t => t.User).Where(t => t.IsDeleted == false).ToList();

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
                    Username = talep.User.UserName,
                    StatusName = talep.Status.Name
                };
                var picture = talep.Images.FirstOrDefault(t => t.TabelaId == talep.Id);

                if (picture != null)
                {
                    var res = talep.Images.FirstOrDefault(t => t.TabelaId == talep.Id);
                    t.Thumbnail = res.PictureUrl;
                }
                model.Add(t);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int tabelaId)
        {
            //parametre ile gelen idye ait tabelayı bul
            var tabela = await _context.Tabelas.Include(t => t.Brand).Include(t => t.Material).Include(t => t.Customer).Include(t => t.Images).FirstOrDefaultAsync(t => t.Id == tabelaId);
            var model = new TabelaUpdateViewModel
            {
                Id = tabelaId,
                Brands = _context.Brands.ToList(),
                BrandId = tabela.BrandId,
                Materials = _context.Materials.ToList(),
                MaterialId = tabela.MaterialId,
                Notes = tabela.Notes,
                CustomerId = tabela.Customer.Id,
                StatusId = tabela.StatusId,
                CreatedDate = tabela.CreatedDate,
                CustomerName = tabela.Customer.Name,
                Pictures = new List<TabelaImages> { }

            };

            foreach (var image in tabela.Images)
            {
                model.Pictures.Add(new TabelaImages { PictureUrl = image.PictureUrl, Id = image.Id });
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(TabelaUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tabela = await _context.Tabelas.Include(t => t.Brand).Include(t => t.Material).Include(t => t.Customer).Include(t => t.Images).FirstOrDefaultAsync(t => t.Id == model.Id);

                tabela.MaterialId = model.MaterialId;
                tabela.BrandId = model.BrandId;
                tabela.Notes = model.Notes;
                tabela.ModifiedDate = DateTime.Now;
                tabela.UserId = 1;
                tabela.CustomerId = model.CustomerId;
                tabela.StatusId = model.StatusId;
                tabela.ModifiedDate = DateTime.Now;

                //eğer yeni fotoğraflar seçildiyse
                if (model.AddedPictures != null)
                {

                    string fileName;
                    //yeni bir tabela images oluştur 
                    //var images = new List<TabelaImages> { };
                    foreach (var item in model.AddedPictures)
                    {
                        //eğer tabelanın statüsü uygulandı ise o zaman upload edilen görselleri uygulama sonrası görseller olarak kaydedeceğiz
                        if(tabela.StatusId == 5)
                        {
                            fileName = await _imageHelper.UploadImageAsync("appliedImages", item);
                        }
                        else
                        {
                            //tüm eklenen görselleri upload et
                            fileName = await _imageHelper.UploadImageAsync("tabelaImages", item);
                        }
                       
                        //images değişkenine tüm görselleri ata
                        tabela.Images.Add(new TabelaImages { PictureUrl = fileName });
                    };

                }
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
        public IActionResult HardDelete(int tabelaId)
        {
            if (tabelaId != 0)
            {
                _context.Tabelas.Remove(new Tabela { Id = tabelaId });
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Tabela silme işlemi başarılı", new ToastrOptions
                {
                    Title = "İşlem Başarılı"
                });
                return NoContent();
            }
            return NotFound();

        }


        [HttpGet]
        public async Task<IActionResult> Delete(int tabelaId)
        {
            //database deki veriyi çek
            var tabela = await _context.Tabelas.FirstOrDefaultAsync(t => t.Id == tabelaId);
            //eğer veritabanında böyle bir kayıt var ise 
            if (tabela != null)
            {
                tabela.IsDeleted = true;
                _context.Update(tabela);
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
            return PartialView("ReadTabelaNotesPartialView", model);
        }

        [HttpPost]
        public IActionResult ImageDelete(int imgId)
        {

            _context.TabelaImages.Remove(new TabelaImages { Id = imgId });
            _context.SaveChanges();
            return NoContent();

        }

        [HttpGet]
        public async Task<IActionResult> TabelaDetail(int tabelaId)
        {
            var tabela = await _context.Tabelas.Include(t => t.Brand).Include(t => t.Material).Include(t => t.Customer).Include(t => t.Images).Include(t => t.Status).FirstOrDefaultAsync(t => t.Id == tabelaId);
            if (tabela != null)
            {
                var listOfString = new List<string>();
                foreach (var images in tabela.Images)
                {
                    listOfString.Add(images.PictureUrl);
                }
                var vm = new TabelaDetailViewModel
                {
                    BrandName = tabela.Brand.Name,
                    CreatedDate = tabela.CreatedDate,
                    CustomerName = tabela.Customer.Name,
                    MaterialName = tabela.Material.Name,
                    Notes = tabela.Notes,
                    StatusName = tabela.Status.Name,
                    TabelaImages = listOfString
                };



                return View(vm);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> DeletedTabelas()
        {

            //databaseden tüm tabelaları çekelim
            var tabelalar = _context.Tabelas.Include(t => t.Brand).Include(t => t.Material).Include(t => t.Status).Include(t => t.Customer).Include(t => t.Images).Where(t => t.IsDeleted == true).ToList();

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

                    StatusName = talep.Status.Name
                };
                var picture = talep.Images.FirstOrDefault(t => t.TabelaId == talep.Id);

                if (picture != null)
                {
                    var res = talep.Images.FirstOrDefault(t => t.TabelaId == talep.Id);
                    t.Thumbnail = res.PictureUrl;
                }
                model.Add(t);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<PartialViewResult> ChangeStatus(int tabelaId)
        {
            var updatedTabela = await _context.Tabelas.Include(t => t.Status).FirstOrDefaultAsync(t => t.Id == tabelaId);
            if (updatedTabela != null)
            {
                var viewmodel = new ChangeStatusViewModel
                {
                    StatusId = updatedTabela.StatusId,
                    StatusName = updatedTabela.Status.Name,
                    Statuses = await _context.Statuses.ToListAsync(),
                    TabelaId = updatedTabela.Id
                };

                return PartialView("ChangeStatusPartialView", viewmodel);
            }
            return PartialView("ChangeStatusPartialView");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(ChangeStatusViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tabela = await _context.Tabelas.Include(t => t.Status).FirstOrDefaultAsync(t => t.Id == model.TabelaId);
                if (tabela != null)
                {
                    tabela.StatusId = model.StatusId;
                    _context.Update(tabela);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> UndoDelete(int tabelaId)
        {
            var tabela = await _context.Tabelas.FirstOrDefaultAsync(t => t.Id == tabelaId);
            if (tabela != null)
            {
                tabela.IsDeleted = false;
                _context.Tabelas.Update(tabela);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return NotFound();
        }
    }
}
