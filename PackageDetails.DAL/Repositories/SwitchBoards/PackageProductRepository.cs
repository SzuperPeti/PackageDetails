using Microsoft.EntityFrameworkCore;
using PackageDetails.CORE.Models.Switchboards;
using PackageDetails.CORE.Repositories.Switchboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageDetails.DAL.Repositories.SwitchBoards
{
    class PackageProductRepository : Repository<PackageProduct>, IPackageProductRepository
    {

        public PackageProductRepository(PackageDetailsContext context)
         : base(context)
        { }

        private PackageDetailsContext _context
        {
            get { return Context as PackageDetailsContext; }
        }

        public async Task<IEnumerable<PackageProduct>> GetAllProductOfPackageAsync(int packageId, int userId)
        {
            return await _context.PackageProduct.Where(x => x.PackageId == packageId).Include(x => x.Product).Include(x => x.Package).ToListAsync();
        }

        //speciális helyzet és az időhiány miatt csak
        public void DeleteDb()
        {
            _context.PackageProduct.RemoveRange(_context.PackageProduct);
        }
    }
}
