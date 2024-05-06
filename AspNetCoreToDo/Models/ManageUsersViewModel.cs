using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreToDo.Models
{
    public class ManageUsersViewModel
    {
        public IdentityUser[] Administrators { get; set; }
        public IdentityUser[] Everyone { get; set; }
    }
}