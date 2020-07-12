using PackageDetails.CORE.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PackageDetails.CORE.Services.Products
{
    public interface IProductService
    {
        Task<IEnumerable<Object>> GetAllProductOfPackage(int packageId, int userId);
    }
}
