using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Mobiles.Models
{
    public class Mobile
    {
        public virtual int MobileId { get; set; }
        [Required]
        [Display(Name="Manufacturer")]
        public virtual int ManufacturerId { get; set; }
        [Required]
        [Display(Name = "Model")]
        public virtual string Model { get; set; }
        [Display(Name="Image")]
        public virtual string Image { get; set; }
        [Display(Name="Price")]
        public virtual decimal Price { get; set; }
        [Display(Name="Is Active")]
        public virtual bool IsActive { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
