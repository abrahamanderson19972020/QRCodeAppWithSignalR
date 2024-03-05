using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfProductDal:GenericRepository<Product>,IProductDal
    {
        public EfProductDal(SignalRContext context):base(context)
        {
            
        }

        public List<Product> GetProductsWithCategoryName()
        {
            var context = new SignalRContext();
            var values = context.Products.Include(p => p.Category).ToList();
            return values;
        }
    }
}
