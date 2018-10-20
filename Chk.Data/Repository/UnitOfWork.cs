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
        private GenericRepository<User> _LocalUserRepository;
        private GenericRepository<Product> _ProductRepository;
        private GenericRepository<Item> _ItemRepository;
        private GenericRepository<Company> _CompanyRepository;
        private GenericRepository<CompanyLocation> _CompanyLocationRepository;
        private GenericRepository<Vendor> _VendorRepository;
        private GenericRepository<Role> _RoleRepository;
        private GenericRepository<StockInProduct> _StockInProductRepository;
        private GenericRepository<StockOut> _StockOutRepository;
        private GenericRepository<LoginHistory> _LoginHistoryRepository;
        private GenericRepository<ErrorLog> _ErrorLogRepository;

        private GenericRepository<SP_StockIn_Result> _stockInReport;
        private GenericRepository<SP_StockOut_Result> _stockoutReport;

        public GenericRepository<SP_StockOut_Result> SP_StockOutRepository
        {
            get
            {
                if (this._stockoutReport == null)
                {
                    this._stockoutReport = new GenericRepository<SP_StockOut_Result>(context);
                }
                return _stockoutReport;
            }
        }
        public GenericRepository<SP_StockIn_Result> SP_StockInRepository
        {
            get
            {
                if (this._stockInReport == null)
                {
                    this._stockInReport = new GenericRepository<SP_StockIn_Result>(context);
                }
                return _stockInReport;
            }
        }
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

        public GenericRepository<User> LocalUserRepository
        {
            get
            {
                if (this._LocalUserRepository == null)
                {
                    this._LocalUserRepository = new GenericRepository<User>(context);
                }
                return _LocalUserRepository;
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

        public GenericRepository<Item> ItemRepository
        {
            get
            {
                if (this._ItemRepository == null)
                {
                    this._ItemRepository = new GenericRepository<Item>(context);
                }
                return _ItemRepository;
            }

        }
        public GenericRepository<Company> CompanyRepository
        {
            get
            {
                if (this._CompanyRepository == null)
                {
                    this._CompanyRepository = new GenericRepository<Company>(context);
                }
                return _CompanyRepository;
            }

        }
        public GenericRepository<CompanyLocation> CompanyLocationRepository
        {
            get
            {
                if (this._CompanyLocationRepository == null)
                {
                    this._CompanyLocationRepository = new GenericRepository<CompanyLocation>(context);
                }
                return _CompanyLocationRepository;
            }

        }
        public GenericRepository<Vendor> VendorRepository
        {
            get
            {
                if (this._VendorRepository == null)
                {
                    this._VendorRepository = new GenericRepository<Vendor>(context);
                }
                return _VendorRepository;
            }

        }
        public GenericRepository<Role> RoleRepository
        {
            get
            {
                if (this._RoleRepository == null)
                {
                    this._RoleRepository = new GenericRepository<Role>(context);
                }
                return _RoleRepository;
            }

        }
        public GenericRepository<StockInProduct> StockInProductRepository
        {
            get
            {
                if (this._StockInProductRepository == null)
                {
                    this._StockInProductRepository = new GenericRepository<StockInProduct>(context);
                }
                return _StockInProductRepository;
            }

        }
        public GenericRepository<StockOut> StockOutRepository
        {
            get
            {
                if (this._StockOutRepository == null)
                {
                    this._StockOutRepository = new GenericRepository<StockOut>(context);
                }
                return _StockOutRepository;
            }

        }
        public GenericRepository<LoginHistory> LoginHistoryRepository
        {
            get
            {
                if (this._LoginHistoryRepository == null)
                {
                    this._LoginHistoryRepository = new GenericRepository<LoginHistory>(context);
                }
                return _LoginHistoryRepository;
            }

        }

        public GenericRepository<ErrorLog> ErrorLogRepository
        {
            get
            {
                if (this._ErrorLogRepository == null)
                {
                    this._ErrorLogRepository = new GenericRepository<ErrorLog>(context);
                }
                return _ErrorLogRepository;
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
