using Microsoft.EntityFrameworkCore;
using PackageDetails.CORE.Models.Products;
using PackageDetails.CORE.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageDetails.DAL.Repositories.Products
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        public ProductRepository(PackageDetailsContext context)
         : base(context)
        { }

        private PackageDetailsContext _context
        {
            get { return Context as PackageDetailsContext; }
        }

        //speciális helyzet és időhiány miatt csak
        public void DeleteDb()
        {
            _context.Product.RemoveRange(_context.Product);
        }

    }
}
