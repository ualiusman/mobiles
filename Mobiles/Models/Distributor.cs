using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mobiles.Models
{
    public class Distributor
    {
        public virtual int DistributorId { get; set; }
        [Required]
        [Display(Name="Distributor Name")]
        public virtual string Name { get; set; }
        [Display(Name="Is Active")]
        public virtual bool IsActive { get; set; }
        public virtual ICollection<Manufacturer> Manufacturers { get; set; }
    }
}