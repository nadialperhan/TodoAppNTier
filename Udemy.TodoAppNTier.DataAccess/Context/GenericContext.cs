using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy.TodoAppNTier.DataAccess.Context
{
    public class GenericContext<T> :TodoContext where T:class,new()/*: AppDbContext where T : class, new()*/
    {
        private readonly string _connectionString;
        public GenericContext(string connectionString) /*: base(connectionString)*/
        {
            _connectionString = connectionString;

        }
        public virtual DbSet<T> Entities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T>(entity =>
            {
                entity.HasNoKey();
            });
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
