using GunShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.ViewModels.UsersViewModel
{
    public class UserViewModel : Customer
    {
        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<string> Roles { get; set; }

        public UserViewModel(Customer c, ApplicationUser u)
        {
            this.ApplicationUserId = c.ApplicationUserId;
            this.Email = c.Email;
            this.Id = c.Id;
            this.IsTemp = c.IsTemp;
            this.ApplicationUser = u;
        }
    }
}
