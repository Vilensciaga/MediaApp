using MediaApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
        //Expose access to savechanges
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        // Expose access to the Database facade
        DatabaseFacade Database { get; }

        // Expose Entry() for entity tracking
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
