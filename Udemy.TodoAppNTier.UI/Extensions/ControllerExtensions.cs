using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Common.ResponseObjects;

namespace Udemy.TodoAppNTier.UI.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult ResponseRedirecttoAction<T>(this Controller controller,IResponse<T> response,string actionname)
        {
            if (response.ResponseType==ResponseType.NotFound)
            {
                return controller.NotFound();
            }
            if (response.ResponseType == ResponseType.ValidationError)
            {
                foreach (var error in response.ValidationErrors)
                {
                    controller.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return controller.View(response.Data);
            }
            return controller.RedirectToAction(actionname);
        }

        public static IActionResult ResponseView<T>(this Controller controller, IResponse<T> response)
        {
            if (response.ResponseType == ResponseType.NotFound)
            {
                return controller.NotFound();
            }
            return controller.View(response.Data);
        }
        public static IActionResult ResponseRedirecttoAction(this Controller controller, IResponse response, string actionname)
        {
            if (response.ResponseType == ResponseType.NotFound)
            {
                return controller.NotFound();
            }
            return controller.RedirectToAction(actionname);
        }
    }
}
