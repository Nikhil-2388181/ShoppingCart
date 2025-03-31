using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Repository.IRepository
{
   public interface IUnitofWork
    {
       ICategoryRepository Category { get; }
       IProductRepository Product { get; }
       IShopCartRepository ShopCart { get; }
       IApplicationUserRepository ApplicationUser { get; }
        IOrderDetailRepository OrderDetail { get; }
        IOrderHeaderRepository OrderHeader { get; }
        void Save();
    }
}
