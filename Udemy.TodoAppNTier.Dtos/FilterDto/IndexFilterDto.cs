using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Udemy.TodoAppNTier.Dtos.FilterDto
{
    public class IndexFilterDto
    {
        public string Definition { get; set; }
        public SelectList drpIsCompleted { get; set; }
        public bool? IsCompleted { get; set; }
        //public SelectList drpDeneme { get; set; }
        //public int SelectedDenemeId { get; set; }
        public int Page { get; set; }
        public string sortingOrder { get; set; }
    }
}
