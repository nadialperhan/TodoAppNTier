using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Udemy.TodoAppNTier.UI.Extensions
{
    public static class GenericExtensions
    {
        public static string GetUrlWithFiltersGeneric<T>(this IUrlHelper urlHelper, string action, string controller, T filter, int? page = null,string sortingOrder = null) where T : class
        {
            var routeValues = new RouteValueDictionary();

            // Model özelliklerini filter. prefix'i ile ekle
            foreach (var prop in typeof(T).GetProperties())
            {
                var value = prop.GetValue(filter);

                // Null olmayan değerleri ekle
                if (value != null)
                {
                    // Page property'sini özel olarak işle
                    if (prop.Name.Equals("Page", StringComparison.OrdinalIgnoreCase))
                    {
                        // Eğer page parametresi verilmişse onu kullan, yoksa modeldeki değeri kullan
                        routeValues["page"] = page ?? Convert.ToInt32(value);
                    }
                    else
                    {
                        //routeValues[$"filter.{prop.Name}"] = value;
                        routeValues[$"{prop.Name}"] = value;
                    }
                }
            }

            // Eğer page parametresi verilmiş ve modelde Page property'si yoksa
            if (page.HasValue && !typeof(T).GetProperties().Any(p => p.Name.Equals("Page", StringComparison.OrdinalIgnoreCase)))
            {
                routeValues["page"] = page.Value;
            }
            // SortingOrder parametresini URL'ye ekle
            if (!string.IsNullOrEmpty(sortingOrder))
            {
                routeValues["sortingOrder"] = sortingOrder;
            }
            return urlHelper.Action(action, controller, routeValues);
        }

    }
}
