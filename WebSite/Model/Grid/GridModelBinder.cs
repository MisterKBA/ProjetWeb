using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Model.Grid
{
    public class GridModelBinder:IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext) 
        {
            if (bindingContext==null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            var draw = Convert.ToInt32(bindingContext.ActionContext.HttpContext.Request.Form["draw"].FirstOrDefault());
            var start = Convert.ToInt32(bindingContext.ActionContext.HttpContext.Request.Form["start"].FirstOrDefault());
            var length = Convert.ToInt32(bindingContext.ActionContext.HttpContext.Request.Form["length"].FirstOrDefault());
            var sortColumn = bindingContext.ActionContext.HttpContext.Request.Form["columns[" + bindingContext.ActionContext.HttpContext.Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = bindingContext.ActionContext.HttpContext.Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = bindingContext.ActionContext.HttpContext.Request.Form["search[value]"].FirstOrDefault();

            var grid = new GridSettings()
            {
                Draw = draw,
                Length = length,
                Search = searchValue,
                SortColumn=sortColumn,
                SortOrder=sortColumnDirection,
                Start=start
            };
            bindingContext.Result = ModelBindingResult.Success(grid);
            return Task.CompletedTask;
        }
    }
}
