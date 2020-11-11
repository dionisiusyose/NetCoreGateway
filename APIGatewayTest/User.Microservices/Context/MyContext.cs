using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Microservices.Models;

namespace User.Microservices.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){ }
        public DbSet<Users> Users { get; set; }
    }
}
