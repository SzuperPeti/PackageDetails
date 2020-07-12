using PackageDetails.CORE.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PackageDetails.CORE.Models.MasterData
{
    [Table("DeliveryStage")]
    public partial class DeliveryStage : BaseEntity
    {
        [StringLength(20)]
        public string Code { get; set; }

        [StringLength(50)]
        public string EnglishStatus { get; set; }

        [StringLength(50)]
        public string HungaryStatus { get; set; }
    }
}
