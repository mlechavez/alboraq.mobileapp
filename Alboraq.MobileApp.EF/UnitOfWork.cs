using Alboraq.MobileApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alboraq.MobileApp.Core.IRepositories;
using Alboraq.MobileApp.EF.Repositories;

namespace Alboraq.MobileApp.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private AlboraqAppContext _ctx;
        private IOrderDetailRepository _orderDetails;
        private IOrderRepository _orders;
        private IProductRepository _products;

        public UnitOfWork(string nameOrConnectionstring)
        {
            _ctx = new AlboraqAppContext(nameOrConnectionstring);
        }
        public IOrderDetailRepository OrderDetails
        {
            get
            {
                return _orderDetails ?? (_orderDetails = new OrderDetailRepository(_ctx));
            }
        }

        public IOrderRepository Orders
        {
            get
            {
                return _orders ?? (_orders = new OrderRepository(_ctx));
            }
        }

        public IProductRepository Products
        {
            get
            {
                return _products ?? (_products = new ProductRepository(_ctx));
            }
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _orderDetails = null;
            _orders = null;
            _products = null;
        }

        public int SaveChanges()
        {
            return _ctx.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _ctx.SaveChangesAsync();
        }
    }
}
