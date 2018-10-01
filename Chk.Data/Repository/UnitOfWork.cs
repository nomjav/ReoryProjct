using Chakwal.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chakwal.Data.Repository
{

    public class UnitOfWork : IDisposable
    {

        private CHK_InventoryEntities context = new CHK_InventoryEntities();

        private GenericRepository<AspNetUser> _UserRepository;
        private GenericRepository<Product> _ProductRepository;
        public GenericRepository<AspNetUser> UserRepository
        {
            get
            {
                if (this._UserRepository == null)
                {
                    this._UserRepository = new GenericRepository<AspNetUser>(context);
                }
                return _UserRepository;
            }

        }

        public GenericRepository<Product> ProductRepository
        {
            get
            {
                if (this._ProductRepository == null)
                {
                    this._ProductRepository = new GenericRepository<Product>(context);
                }
                return _ProductRepository;
            }

        }
        public void Save()
        {
            context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }


}
