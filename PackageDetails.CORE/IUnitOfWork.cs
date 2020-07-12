using PackageDetails.CORE.Repositories.Identity;
using PackageDetails.CORE.Repositories.MasterData;
using PackageDetails.CORE.Repositories.Packages;
using PackageDetails.CORE.Repositories.Products;
using PackageDetails.CORE.Repositories.Switchboards;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PackageDetails.CORE
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthRepository Identity { get; }
        IProductRepository Product { get; }
        IPackageRepository Package { get; }
        IMasterDataRepository MasterData { get; }
        IPackageProductRepository PackageProduct { get; }



        Task<int> CommitAsync();
    }
}
