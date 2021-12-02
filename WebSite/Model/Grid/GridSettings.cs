using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Model.Grid
{
    [ModelBinder(BinderType=typeof(GridModelBinder))]
    public class GridSettings
    {
        public string Search { get; set; }
        public int Draw { get; set; }
        public int Length { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public int Start { get; set; }
    }
}
