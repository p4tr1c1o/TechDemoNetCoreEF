using BackendDemo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace BackendDemo.Infrastructure
{
    public class BackendDemoContext : DbContext
    {
        private IDbContextTransaction _currentTransaction;

        public BackendDemoContext(DbContextOptions options)
            : base(options)
        {        }

        public DbSet<Producto> Productos{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().HasData(
              new Producto { Id = 1, Nombre = "Producto1", Descripcion = "descripcion producto 1" },
              new Producto { Id = 2, Nombre = "Producto2", Descripcion = "descripcion producto 2" },
              new Producto { Id = 3, Nombre = "Producto3", Descripcion = "descripcion producto 3" },
              new Producto { Id = 4, Nombre = "Producto4", Descripcion = "descripcion producto 4" },
              new Producto { Id = 5, Nombre = "Producto5", Descripcion = "descripcion producto 5" }
              );

        }


            
            #region Transaction Handling
            public void BeginTransaction()
        {
            if (_currentTransaction != null)
            {
                return;
            }

            if (!Database.IsInMemory())
            {
                _currentTransaction = Database.BeginTransaction(IsolationLevel.ReadCommitted);
            }
        }

        public void CommitTransaction()
        {
            try
            {
                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
        #endregion
    }
}
