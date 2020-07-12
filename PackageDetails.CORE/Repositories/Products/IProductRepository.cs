using PackageDetails.CORE.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PackageDetails.CORE.Repositories.Products
{
    public interface IProductRepository : IRepository<Product>
    {
        void DeleteDb();
    }
}
