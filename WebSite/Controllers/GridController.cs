using Microsoft.AspNetCore.Mvc;
using NasaImage.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using WebSite.Model.Grid;
using WebSite.Model;
using Microsoft.AspNetCore.Authorization;

namespace WebSite.Controllers
{
    public class GridController : Controller
    {

        public ITodoServiceDate _todoService;

        public GridController(ITodoServiceDate todoService)
        {
            _todoService = todoService;
        }
        [Authorize]
        public IActionResult ShowGrid()
        {
            return View();
            
        }
        [Authorize]
        [HttpPost]    
        public JsonResult LoadData(GridSettings grid)
        {
            try
            {
                int recordsTotal = 0;
                var DateData = (from tempDate in _todoService.GetAllForGrid() select tempDate);

                //Sorting
                if (!(string.IsNullOrEmpty(grid.SortColumn) && string.IsNullOrEmpty(grid.SortOrder)))
                {
                    DateData = DateData.OrderBy(grid.SortColumn + " " + grid.SortOrder);
                }

                //Search
                if (!string.IsNullOrEmpty(grid.Search))
                {
                    DateData = DateData.Where(m => m.date == grid.Search);
                }

                //total number of rows count 
                recordsTotal = DateData.Count();
                //Pagination 
                var data = DateData.Skip(grid.Start).Take(grid.Length).ToList();
                return Json(new { draw = grid.Draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = GenerateJson(data) });
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static string[][] GenerateJson(List<NasaImage.Model.TodoDateId> todoDates)
        {
            if (todoDates == null || !todoDates.Any())
            {
                return (from i in new List<NasaImage.Model.TodoDateId>()
                        select new string[]
                    {
                    }).ToArray();
            }


            var Res = (from i in todoDates
                       select new string[]
                {
                    i.id.ToString(),
                    i.date

                }).ToArray();
            return Res;
        }

    }

}
