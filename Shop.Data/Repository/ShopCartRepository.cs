using Shop.Data.Data;
using Shop.Data.Repository.IRepository;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Repository
{
    public class ShopCartRepository : Repository<ShopCart>, IShopCartRepository
    {
        private ApplicationDbContext _db;
        public ShopCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

       


        public void Update(ShopCart obj)
        {
            _db.ShopCarts.Update(obj);
        }
    }
}
