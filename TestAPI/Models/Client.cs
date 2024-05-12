using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ClientsProject.Models
{
    public class Client
    {
        public int ID { get; set; }

        [DisplayName("First Name")]
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [DisplayName("Middle Name")]
        [StringLength(30)]
        public string MiddleName { get; set; }

        [DisplayName("Last Name")]
        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [StringLength(15, MinimumLength = 3)]
        [Required]
        //[RegularExpression("^[0-9\\-\\+]{9,15}$", ErrorMessage = "Please, enter your phone correct")]
        [Phone]
        public string Phone { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        [EmailAddress]
        //[RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Please, enter your email correct")]
        public string Email { get; set; }

        
    }
    public class ClientDBContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
    }
}