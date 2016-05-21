using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HungryDevs.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User name is mandatory field!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is mandatory field!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool IsAdmin { get; set; }
    }
}