using Alboraq.MobileApp.Core;
using System.Threading.Tasks;
using Alboraq.MobileApp.Core.IRepositories;
using Alboraq.MobileApp.EF.Repositories;

namespace Alboraq.MobileApp.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private AlboraqAppContext _ctx;
        private IAppointmentRepository _appointments;
        private IOrderDetailRepository _orderDetails;
        private IOrderRepository _orders;
        private IProductRepository _products;
        private IProductCategoryRepository _productCategories;

        public UnitOfWork(string nameOrConnectionstring)
        {
            _ctx = new AlboraqAppContext(nameOrConnectionstring);
        }

        public IAppointmentRepository Appointments
        {
            get
            {
                return _appointments ?? (_appointments = new AppointmentRepository(_ctx));
            }
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

        public IProductCategoryRepository ProductCategories
        {
            get
            {
                return _productCategories = (_productCategories = new ProductCategoryRepository(_ctx));
            }
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _appointments = null;
            _orderDetails = null;
            _orders = null;
            _products = null;
            _productCategories = null;
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
