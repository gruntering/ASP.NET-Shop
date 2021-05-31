using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hub.TablesRetriever
{
    // Interface for Operating with Materials 
    public interface IMaterialRepository
    {
        IEnumerable<Material> Materials { get;  }
        void SaveChanges(Material material);
        Material DeleteMaterial(int materialID);
    }
}
