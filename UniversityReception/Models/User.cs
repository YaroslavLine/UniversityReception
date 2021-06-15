using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityReception.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserLogin { get; set; }
        public string Password { get; set; }
        public DateTime DateOfCreating { get; set; }
        public Role UserRole { get; set; }
    }
}
