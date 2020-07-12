using PackageDetails.CORE;
using PackageDetails.CORE.Models.Products;
using PackageDetails.CORE.Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageDetails.BLL.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Object>> GetAllProductOfPackage(int packageId, int userId)
        {
            var packageProducts = await _unitOfWork.PackageProduct.GetAllProductOfPackageAsync(packageId, userId);

            if (packageProducts.Any(x => x.Package.UserId == userId))
            {
                return packageProducts.Select(x => new { x.Product.Name, x.ProductCount });
            }
            return null;
        }
    }
}
