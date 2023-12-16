using Core.DataAccess.SQLServer.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFOrderDAL : EFRepositoryBase<Order, AppDbContext>, IOrderDAL
    {
        public void AddRange(List<Order> orders)
        {
            using var context = new AppDbContext();

            // Siparişleri eklemek için veritabanına bağlan
            context.AddRange(orders);

            foreach (var order in orders)
            {
                // Siparişin miktarını stoktan çıkar
                var product = context.Products.FirstOrDefault(p => p.Id == order.ProductId);
                if (product != null)
                {
                    product.Quantity -= 1;
                }
            }
      

            // Değişiklikleri kaydet
            context.SaveChanges();
        }
    }
}
