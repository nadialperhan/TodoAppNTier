using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Dtos.FilterDto;

namespace Udemy.TodoAppNTier.UI.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string GetUrlWithFilters(this IUrlHelper urlHelper, string actionName, string controllerName, IndexFilterDto filter)
        {
            return urlHelper.Action(actionName, controllerName, new
            {
                page = filter.Page,
                Definition = filter.Definition,
                IsCompleted = filter.IsCompleted
            });
        }
    }
}
