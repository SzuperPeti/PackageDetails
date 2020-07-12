using System;
using System.Collections.Generic;
using System.Text;

namespace PackageDetails.CORE.Interfaces
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        bool IsDeleted { get; set; }
        string CreatedUser { get; set; }
        DateTime CreateDate { get; set; }
        string ModifiedUser { get; set; }
        DateTime? ModifyDate { get; set; }
    }
}
