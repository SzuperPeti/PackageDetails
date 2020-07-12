using PackageDetails.CORE.Models.MasterData;
using PackageDetails.CORE.Repositories.MasterData;
using System;
using System.Collections.Generic;
using System.Text;

namespace PackageDetails.DAL.Repositories.MasterData
{
    class MasterDataRepository : Repository<DeliveryStage>, IMasterDataRepository
    {

        public MasterDataRepository(PackageDetailsContext context)
         : base(context)
        { }

        private PackageDetailsContext _context
        {
            get { return Context as PackageDetailsContext; }
        }

    }
}
