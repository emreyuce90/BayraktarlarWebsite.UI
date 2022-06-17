﻿using AutoMapper;
using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.DAL.Context;
using BayraktarlarWebsite.Entities.Dtos;
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
        private readonly IStatusService _statusService;
        private readonly ITabelaImagesService _tabelaImagesService;
        private readonly ITabelaService _tabelaService;
        private readonly IMaterialService _materialService;
        private readonly IBrandService _brandService;
        private readonly IToastNotification _toastNotification;
        private readonly IImageHelper _imageHelper;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        public TabelaController(IToastNotification toastNotification, IImageHelper imageHelper, ICustomerService customerService, IBrandService brandService, IMaterialService materialService, ITabelaService tabelaService, IMapper mapper, ITabelaImagesService tabelaImagesService, IStatusService statusService)
        {

            _toastNotification = toastNotification;
            _imageHelper = imageHelper;
            _customerService = customerService;
            _brandService = brandService;
            _materialService = materialService;
            _tabelaService = tabelaService;
            _mapper = mapper;
            _tabelaImagesService = tabelaImagesService;
            _statusService = statusService;
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //customerlistdto
            var customers = await _customerService.GetAllAsync();
            //brandlistdto
            var brands = await _brandService.GetAllAsync();
            //materiallistdto
            var materials = await _materialService.GetAllAsync();


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
                var table = new TabelaAddDto
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
                    await _tabelaService.AddAsync(table);

                }

                _toastNotification.AddSuccessToastMessage("Tabela ekleme işlemi başarılı");
                return RedirectToAction("Index");
            }
            else
            {
                var customers = await _customerService.GetAllAsync();
                //markalar
                var brands = await _brandService.GetAllAsync();
                //malzemeler
                var materials = await _materialService.GetAllAsync();
                //modelleri dolu olarak view a return eder
                _toastNotification.AddErrorToastMessage("Bir hata meydana geldi", new ToastrOptions
                {
                    Title = "Hata"
                });
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
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //db deki tabelalar
            var tabelalar = await _tabelaService.GetAllAsync();
            //vm
            List<TabelaViewModel> model = new List<TabelaViewModel>();

            //databasedeki verileri gezelim ve boş olan modelimize ekleyelim
            foreach (var talep in tabelalar.Tabela)
            {
                var t = new TabelaViewModel
                {
                    Id = talep.Id,
                    BrandName = talep.Brand.Name,
                    CreatedDate = talep.CreatedDate,
                    CustomerName = talep.Customer.Name,
                    MaterialName = talep.Material.Name,
                    Notes = talep.Notes,
                    Username = "1",
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
            //tabela dto
            var tabela = await _tabelaService.GetTabelaByTabelaIdAsync(tabelaId);
            var tbl = new TabelaUpdateDto
            {
                BrandId = tabela.Tabela.BrandId,
                Brands = await _brandService.GetAllAsync(),
                Id = tabela.Tabela.Id,
                MaterialId = tabela.Tabela.MaterialId,
                Materials = await _materialService.GetAllAsync(),
                Notes = tabela.Tabela.Notes,
                Images = (List<TabelaImages>)tabela.Tabela.Images

            };
            return View(tbl);
        }

        [HttpPost]
        public async Task<IActionResult> Update(TabelaUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                //eğer yeni fotoğraflar seçildiyse
                if (model.AddedPictures != null)
                {
                    //TabelaImages listesi oluştur
                    var tablImages = new List<TabelaImages> { };
                    string fileName;
                    //yeni bir tabela images oluştur 
                    //var images = new List<TabelaImages> { };
                    foreach (var item in model.AddedPictures)
                    {
                        //eğer tabelanın statüsü uygulandı ise o zaman upload edilen görselleri uygulama sonrası görseller olarak kaydedeceğiz
                        var applied = await _tabelaService.GetStatusCodeGivenTabelaAsync(model.Id);
                        if (applied)
                        {
                            fileName = await _imageHelper.UploadImageAsync("appliedImages", item);
                        }
                        else
                        {
                            //tüm eklenen görselleri upload et
                            fileName = await _imageHelper.UploadImageAsync("tabelaImages", item);
                        }
                        //upload edilen görsellerin hepsini Pictures nesnesine ekle
                        var tbl = new TabelaImages { PictureUrl = fileName };
                        tablImages.Add(tbl);
                       
                    };
                    model.Images = tablImages;

                }
                await _tabelaService.UpdateAsync(model);

                _toastNotification.AddSuccessToastMessage("Tabela güncelleme işlemi başarılı", new ToastrOptions
                {
                    Title = "Güncelleme Başarılı"
                });
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> HardDelete(int tabelaId)
        {
            if (tabelaId != 0)
            {
                await _tabelaService.HardDeleteAsync(tabelaId);
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
            var tabela = await _tabelaService.GetTabelaByTabelaIdAsync(tabelaId);
            //eğer veritabanında böyle bir kayıt var ise 
            if (tabela != null)
            {
                tabela.Tabela.IsDeleted = true;
                await _tabelaService.UpdateAsync(_mapper.Map<TabelaUpdateDto>(tabela));
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
            var note = await _tabelaService.GetTabelaByTabelaIdAsync(tabelaId);
            var model = new NoteViewModel();
            model.Note = note.Tabela.Notes;
            return PartialView("ReadTabelaNotesPartialView", model);
        }

        [HttpPost]
        public async Task<IActionResult> ImageDelete(int imgId)
        {
            if (imgId != 0)
            {
                await _tabelaImagesService.RemoveAsync(imgId);
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> TabelaDetail(int tabelaId)
        {
            var tabela = await _tabelaService.GetTabelaByTabelaIdAsync(tabelaId);
            if (tabela != null)
            {
                var listOfString = new List<string>();
                foreach (var images in tabela.Tabela.Images)
                {
                    listOfString.Add(images.PictureUrl);
                }
                var vm = new TabelaDetailViewModel
                {
                    BrandName = tabela.Tabela.Brand.Name,
                    CreatedDate = tabela.Tabela.CreatedDate,
                    CustomerName = tabela.Tabela.Customer.Name,
                    MaterialName = tabela.Tabela.Material.Name,
                    Notes = tabela.Tabela.Notes,
                    StatusName = tabela.Tabela.Status.Name,
                    TabelaImages = listOfString
                };



                return View(vm);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> DeletedTabelas()
        {
            var tabelalar = await _tabelaService.DeletedTabelasAsync();
            return View(_mapper.Map<TabelaViewModel>(tabelalar));
        }

        [HttpGet]
        public async Task<PartialViewResult> ChangeStatus(int tabelaId)
        {
            var updatedTabela = await _tabelaService.GetTabelaByTabelaIdAsync(tabelaId);
            if (updatedTabela != null)
            {
                var viewmodel = new ChangeStatusViewModel
                {
                    StatusId = updatedTabela.Tabela.StatusId,
                    StatusName = updatedTabela.Tabela.Status.Name,
                    Statuses = await _statusService.GetAllAsync(),
                    TabelaId = updatedTabela.Tabela.Id
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
                var tabela = await _tabelaService.GetTabelaByTabelaIdAsync(model.TabelaId);
                if (tabela != null)
                {
                    tabela.Tabela.StatusId = model.StatusId;
                    await _tabelaService.UpdateAsync(_mapper.Map<TabelaUpdateDto>(tabela));

                    return NoContent();
                }

            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> UndoDelete(int tabelaId)
        {
            var tabela = await _tabelaService.GetTabelaByTabelaIdAsync(tabelaId);
            if (tabela != null)
            {
                tabela.Tabela.IsDeleted = false;
                await _tabelaService.UpdateAsync(_mapper.Map<TabelaUpdateDto>(tabela));
                return NoContent();
            }
            return NotFound();
        }
    }
}
