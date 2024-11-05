using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Common.ResponseObjects;
using Udemy.TodoAppNTier.DataAccess.Context;
using Udemy.TodoAppNTier.Dtos.FilterDto;
using Udemy.TodoAppNTier.Dtos.WorkDtos;
using Udemy.TodoAppNTier.UI.Extensions;
using Udemy.TodoAppNTier.UI.FilterViewModel;
using Udemy.TodoAppNTier.UI.ViewModel;
using Udemy.TodoAppNTierBusiness.Interfaces;
using Udemy.TodoAppNTierBusiness.Services;
using X.PagedList;

namespace Udemy.TodoAppNTier.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWorkService _workService;
        private readonly ICommonService _commonService;
        //private readonly IMemoryCache _memoryCache; // IMemoryCache'i ekleyin

        private static bool _isDropdownDataLoaded = false;
        private static SelectList _dropDownData;
        private static SelectList _dropDownCompleted;


        public HomeController(IWorkService workService, ICommonService commonService, IMemoryCache memoryCache)
        {
            _workService = workService;
            _commonService = commonService;
            //_memoryCache = memoryCache;

        }
        public async Task DropList()
        {
             if (!_isDropdownDataLoaded)
            {
                var result = await _commonService.DropDownParameter(2);
                // Verileri SelectList'e çevir
                _dropDownData = new SelectList(result.Data, "Id", "Text");

                var result2= await _commonService.DropDownParameter(3);
                _dropDownCompleted= new SelectList(result2.Data, "Id", "Text");
                _isDropdownDataLoaded = true;
            }

        }
        public async Task<IActionResult> Index(IndexFilterDto filter, int page = 1)
        {
            await DropList(); // DropDown verisini initialize et

            // Filter'a atama yap
            var akkk = _dropDownData;
           // var cacheKey = "DropdownData";
            //if (!_memoryCache.TryGetValue(cacheKey, out SelectList dropdownData))
            //{
            //    // Eğer cache'de yoksa, veritabanından çek
            //    var result = await _commonService.DropDownParameter(2);
            //    dropdownData = new SelectList(result.Data, "Id", "Text");

            //    // Veriyi cache'e ekle
            //    var cacheEntryOptions = new MemoryCacheEntryOptions()
            //        .SetSlidingExpiration(TimeSpan.FromMinutes(10)); // 10 dakika süreyle cache'de sakla

            //    _memoryCache.Set(cacheKey, dropdownData, cacheEntryOptions);
            //}

            // Filtreye dropdown verisini atayın
            filter.drpDeneme = _dropDownData;
            filter.drpIsCompleted = _dropDownCompleted;
            //burda viewstatelerden biriyle bi kere alıp null değilse burayı atlarım
            //var b = await _commonService.DropDownParameter(2);
            //filter.drpDeneme = new SelectList(b.Data, "Id", "Text");//ındexFilterDto.drpDeneme;
            TempData["Filter"] = filter;
            var a = await _workService.GetWorkDataUsingStoredProcedure(filter);
            

            var pagedList = a.Data.ToPagedList(page, 5);

            var newFilter = new IndexFilterDto
            {
                Definition = filter.Definition,
                IsCompleted = filter.IsCompleted,
                SelectedDenemeId = filter.SelectedDenemeId,
                drpDeneme = _dropDownData,
                drpIsCompleted = _dropDownCompleted,
                Page=page
            };

            WorkFilterModel workFilterModel = new WorkFilterModel()
            {
                WorkListDto = pagedList,
                IndexFilterDto = newFilter

            };
            return View(workFilterModel);
            //return View(pagedList);                       
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new WorkCreateDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create(WorkCreateDto dto)
        {
            //nadi 
            //bakcam daha sonra
            var reponse = await _workService.Create(dto);
            return this.ResponseRedirecttoAction(reponse, "Index");
            //if (reponse.ResponseType==ResponseType.ValidationError)
            //{
            //    foreach (var item in reponse.ValidationErrors)
            //    {
            //        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            //    }
            //    return View(dto);
            //}
            //else 
            //{
            //    return RedirectToAction("Index");
            //}
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var response = await _workService.GetbyID<WorkUpdatedDto>(id);
            return this.ResponseView(response);
            //if (response.ResponseType==ResponseType.NotFound)
            //{
            //    return NotFound();
            //}
            //return View(response.Data);
        }
        [HttpPost]

        public async Task<IActionResult> Update(WorkUpdatedDto dto)
        {
            var response = await _workService.Update(dto);
            return this.ResponseRedirecttoAction(response, "Index");
            //if (response.ResponseType==ResponseType.ValidationError)
            //{
            //    foreach (var item in response.ValidationErrors)
            //    {
            //        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            //    }
            //    return View(dto);
            //}
            //return RedirectToAction("Index");
        }
        public async Task<IActionResult> Remove(int id)
        {
            var response = await _workService.Remove(id);
            return this.ResponseRedirecttoAction(response, "Index");
            //if (response.ResponseType == ResponseType.NotFound)
            //{
            //    return NotFound();
            //}
            //return RedirectToAction("Index");
        }
        public IActionResult NotFound(int code)
        {
            return View();
        }
        //public async Task< IActionResult> Deneme()
        //{
        //    int id = 2;
        //    var a=await _context.Database.ExecuteSqlRawAsync("EXEC sp_Deneme @id", id);
        //    return View();
        //}
    }
}
