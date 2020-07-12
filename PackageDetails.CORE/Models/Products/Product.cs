using PackageDetails.CORE.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PackageDetails.CORE.Models.Products
{
    [Table("Product")]
    public partial class Product : BaseEntity
    {
        [StringLength(200)]
        public string Name { get; set; }

    }
}
