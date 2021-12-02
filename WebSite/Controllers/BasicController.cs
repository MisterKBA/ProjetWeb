using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NasaImage.Interface;
using NasaImage.Model; 


namespace WebSite.Controllers
{
    
    public class BasicController : Controller
    {
        public ITodoServiceDate _todoService;

        public BasicController(ITodoServiceDate todoService) 
        {
            _todoService = todoService;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {   
            var result = await _todoService.GetAllAsync();
            return View(result);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(string date) 
        {
            var result = await _todoService.GetTodoResultAsync(date);
            return View(result);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> VueOnImage(string identifier)
        {
            var result = await _todoService.GetTodoImageResult(identifier);
            return View(result);
        }

    }
}
