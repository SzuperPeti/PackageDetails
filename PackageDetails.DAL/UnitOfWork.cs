using PackageDetails.CORE;
using PackageDetails.CORE.Repositories.Identity;
using PackageDetails.CORE.Repositories.MasterData;
using PackageDetails.CORE.Repositories.Packages;
using PackageDetails.CORE.Repositories.Products;
using PackageDetails.CORE.Repositories.Switchboards;
using PackageDetails.DAL.Repositories.Identity;
using PackageDetails.DAL.Repositories.MasterData;
using PackageDetails.DAL.Repositories.Packages;
using PackageDetails.DAL.Repositories.Products;
using PackageDetails.DAL.Repositories.SwitchBoards;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PackageDetails.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PackageDetailsContext _context;
        public UnitOfWork(PackageDetailsContext context)
        {
            _context = context;
        }



        private AuthRepository _authRepository;
        private ProductRepository _productRepository;
        private PackageRepository _packageRepository;
        private MasterDataRepository _masterDataRepository;
        private PackageProductRepository _packageProductRepository;




        public IAuthRepository Identity => _authRepository = _authRepository ?? new AuthRepository(_context);
        public IProductRepository Product => _productRepository = _productRepository ?? new ProductRepository(_context);
        public IPackageRepository Package => _packageRepository = _packageRepository ?? new PackageRepository(_context);
        public IMasterDataRepository MasterData => _masterDataRepository = _masterDataRepository ?? new MasterDataRepository(_context);
        public IPackageProductRepository PackageProduct => _packageProductRepository = _packageProductRepository ?? new PackageProductRepository(_context);


        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
