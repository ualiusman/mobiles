using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mobiles.Models
{
    public class Order
    {
        [Key]
        public virtual int OrderId { get; set; }
        [Required]
        [Display(Name="Model")]
        public virtual int MobileId { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name="Client Name")]
        public virtual string ClientName { get; set; }
        [Required]
        [StringLength(100)]
        [EmailAddress]
        [Display(Name="Email")]
        public virtual string Email { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name="Phone")]
        public virtual string Phone { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name="IMEI")]
        public virtual string IMEI { get; set; }
        [Display(Name="Status")]
        [StringLength(20)]
        public virtual string Status { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual Mobile Model { get; set; }
    }
}