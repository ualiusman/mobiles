using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Mobiles.Models
{
    public class Manufacturer
    {
        public virtual int ManufacturerId { get; set; }
        [Required]
        [Display(Name="Distributor")]
        public virtual int DistributorId { get; set; }
        [Required]
        [Display(Name="Manufacturer Name")]
        public virtual string Name { get; set; }
        [Display(Name="Is Active")]
        public virtual bool IsActive { get; set; }
        public virtual Distributor Distributor { get; set; }
        public virtual ICollection<Mobile> Mobiles { get; set; }
    }
}
