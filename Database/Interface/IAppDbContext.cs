using MediaApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Interface
{
    public interface IAppDbContext
    {
        public DbSet<AppUser> Users { get; set; }
    }
}
