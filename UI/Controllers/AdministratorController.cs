using Hub;
using Hub.TablesRetriever;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{   
    public class AdministratorController : Controller
    {
        IMaterialRepository repository; 

        public AdministratorController(IMaterialRepository repos)
        {
            this.repository = repos; 
        }
        //Returns the view of initial administrator page
        public ViewResult Index()
        {
            return View(repository.Materials);
        }
        //Go to editting page when click on the title of the material
        public ViewResult Edit(int materialID)
        {
            Material material = repository.Materials.FirstOrDefault(m => m.MaterialID == materialID);
               
            return View(material);
        }
        //Add Material
        public ViewResult Create()
        {
            return View("Edit", new Material());
        }
        //Delete Material
        [HttpPost]
        public ActionResult Delete(int materialID)
        {
            Material deletedMaterial = repository.DeleteMaterial(materialID);
            if(deletedMaterial != null)
            {
                TempData["message"] = string.Format("Material with Name: {0} was deleted from the list", deletedMaterial.Name);
            }
            return RedirectToAction("Index");
        }
        
         //Confirm Changes when the editting was made
        [HttpPost]
        public ActionResult Edit(Material material, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {   if(image != null)
                {
                    material.ImageMimeType = image.ContentType;
                    material.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(material.ImageData, 0, image.ContentLength);
                }
                repository.SaveChanges(material);
                TempData["message"] = string.Format("Changes to the \"{0}\" are saved", material.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(material);
            }
        }
    }
}