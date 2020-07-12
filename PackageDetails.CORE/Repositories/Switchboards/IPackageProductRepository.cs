using PackageDetails.CORE.Models.Switchboards;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PackageDetails.CORE.Repositories.Switchboards
{
    public interface IPackageProductRepository : IRepository<PackageProduct>
    {
        Task<IEnumerable<PackageProduct>> GetAllProductOfPackageAsync(int packageId, int userId);
        void DeleteDb();
    }
}
