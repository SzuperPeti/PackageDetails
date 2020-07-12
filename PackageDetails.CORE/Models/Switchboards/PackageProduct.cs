using PackageDetails.CORE.Base;
using PackageDetails.CORE.Models.Packages;
using PackageDetails.CORE.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PackageDetails.CORE.Models.Switchboards
{
    [Table("PackageProduct")]
    public partial class PackageProduct : BaseEntity
    {
        public int PackageId { get; set; }

        public int ProductId { get; set; }

        public int ProductCount { get; set; }




        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("PackageId")]
        public virtual Package Package { get; set; }
    }
}
