using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseProject.Models
{
    public class User
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public char Role { get; set; }
        public string Password { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}

