using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Models.Interfaces
{
    public interface ICategory
    {
        int Id { get; set; }
        int? MasterCategoryId { get; set; }
        string Name { get; set; }        
    }
}
