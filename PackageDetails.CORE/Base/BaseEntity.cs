using PackageDetails.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PackageDetails.CORE.Base
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        [StringLength(50)]
        public string CreatedUser { get; set; }


        public DateTime CreateDate { get; set; }


        [StringLength(50)]
        public string ModifiedUser { get; set; }


        public DateTime? ModifyDate { get; set; }


        public bool IsDeleted { get; set; }
    }
}
