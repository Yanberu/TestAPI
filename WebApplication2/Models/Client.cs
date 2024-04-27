using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace ClientsProject.Models
{
    public class Client
    {
        public int ID { get; set; }

        
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        
        [StringLength(30)]
        public string MiddleName { get; set; }

        
        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [StringLength(15, MinimumLength = 3)]
        [Required]
        public string Phone { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Email { get; set; }

    }
    public class ClientDBContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
    }
}