using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class PagesInfo
    {
        public int TotalItems { get; set; } // Total Number of Books 
        public int ItemsPerPage { get; set; } // Number of Books per Page 
        public int CurrentPage { get; set; } // Number of Current Page
        public int TotalPages               // Overall Number of Pages 
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }

    }
}