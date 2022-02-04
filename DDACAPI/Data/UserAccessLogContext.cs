using DDACAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDACAPI.Data
{
    public class UserAccessLogContext : DbContext
    {

        public  UserAccessLogContext(DbContextOptions<UserAccessLogContext> options) : base(options)
        {
        }
        public DbSet<UserAccessLog> UserAccessLog { get; set; }
        public DbSet<User> User { get; set; }

    }
}
