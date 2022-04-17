using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeFirstApp.Models;

namespace CodeFirstApp.Context
{
    public class UserDbContext:DbContext
    {
        public UserDbContext(DbContextOptions options)
        {
        }
        public DbSet<UserLogin> Ulogins { get; set; }
        public DbSet<CodeFirstApp.Models.ModelApp> ModelApp { get; set; }
    }
}
