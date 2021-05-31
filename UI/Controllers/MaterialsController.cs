using Hub;
using Hub.TablesRetriever;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models;


namespace UI.Controllers
{
    public class MaterialsController : Controller
    {
        private IMaterialRepository repository;
        public int pageSize = 5;
        public MaterialsController(IMaterialRepository repo)
        {
            this.repository = repo;
        }
        public ViewResult List(string category, int page = 1)
        {
            MaterialsViewModel model = new MaterialsViewModel
            {
                Materials = repository.Materials
                .Where(b => category == null || b.Subject == category)
                .OrderBy(b => b.MaterialID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),

                PagesInfo = new PagesInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                    repository.Materials.Count() :
                    repository.Materials.Where(material => material.Subject == category).Count()
                },
                CurrentCategory = category
            };



            return View(model);

        }
        public FileContentResult GetImage(int materialID)
        {
            Material material = repository.Materials
            .FirstOrDefault(p => p.MaterialID == materialID);
            if (material != null)
            {
                return File(material.ImageData, material.ImageMimeType);
            }
            else
            {
                return null;
            }
        }


    }
}