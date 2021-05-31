using Hub.TablesRetriever;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hub.Context
{
    public class EFMaterialRepository: IMaterialRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<Material> Materials
        {
            get { return context.Materials; }
        }
        // Saving Changes 
        public void SaveChanges(Material material)
        {
            if(material.MaterialID == 0)
            {
                context.Materials.Add(material);
            }
            else
            {
                Material dbChanges = context.Materials.Find(material.MaterialID);

                if(dbChanges != null)
                {
                    dbChanges.Name = material.Name;
                    dbChanges.Author = material.Author;
                    dbChanges.Portrayal = material.Portrayal;
                    dbChanges.Subject = material.Subject;
                    dbChanges.Cost = material.Cost;
                    dbChanges.ImageData = material.ImageData;
                    dbChanges.ImageMimeType = material.ImageMimeType;

                }
            }
            context.SaveChanges();
        }
        // Delete Specific Material 
        public Material DeleteMaterial(int materialID)
        {
            Material dbChanges = context.Materials.Find(materialID);
            if(dbChanges != null)
            {
                context.Materials.Remove(dbChanges);
                context.SaveChanges();
            }
            return dbChanges;
        }
    }
}
