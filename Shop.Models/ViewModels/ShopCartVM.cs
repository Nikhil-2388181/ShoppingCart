using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models.ViewModels
{
    public class ShopCartVM
    {
     public IEnumerable<ShopCart> ShopCartList { get; set; }

        //public OrderDetail OrderDetail { get; set; }

        public OrderHeader OrderHeader { get; set; }
    
    }
}
