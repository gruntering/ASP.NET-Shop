using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hub.Context
{
    public class EFDbContext: DbContext 
    {
        //Connecting to Materials Database 
        public DbSet<Material> Materials { get; set; }
    }
}
