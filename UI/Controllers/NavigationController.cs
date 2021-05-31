using Hub.TablesRetriever;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class NavigationController : Controller
    {
        // GET: Navigation
        private IMaterialRepository repository;

        public NavigationController(IMaterialRepository repos)
        {
            repository = repos;
        }
         public PartialViewResult Menu(string category = null, bool horizontalView = false)
        {
            ViewBag.SelectedCategory = category;
       

            IEnumerable<string> categories = repository.Materials
                 .Select(material => material.Subject)
                 .Distinct()
                 .OrderBy(x => x);
            string viewName = horizontalView ? "HorizontalMenu" : "Menu";
            return PartialView(viewName,categories);
        }
    }
}