using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Common.ResponseObjects;
using Udemy.TodoAppNTier.DataAccess.Context;
using Udemy.TodoAppNTier.Dtos.WorkDtos;
using Udemy.TodoAppNTier.UI.Extensions;
using Udemy.TodoAppNTierBusiness.Services;

namespace Udemy.TodoAppNTier.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWorkService _workService;

        public HomeController(IWorkService workService)
        {
            _workService = workService;
        }
        public async Task<IActionResult> Index()
        {
            var a=await _workService.GetWorkDataUsingStoredProcedure();
            var response = await _workService.GetAll();          
            return View(response.Data);                       
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
            var reponse=await _workService.Create(dto);
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
            var response=await _workService.Update(dto);
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
            var response=await _workService.Remove(id);
            return this.ResponseRedirecttoAction(response, "Index");
            //if (response.ResponseType == ResponseType.NotFound)
            //{
            //    return NotFound();
            //}
            //return RedirectToAction("Index");
        }
        public  IActionResult NotFound(int code)
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
