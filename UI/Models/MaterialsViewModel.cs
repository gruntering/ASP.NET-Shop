using Hub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class MaterialsViewModel
    {
        public PagesInfo PagesInfo { get; set; }
        public IEnumerable<Material> Materials { get; set; }
        public string CurrentCategory { get; set; }
        
    }
}