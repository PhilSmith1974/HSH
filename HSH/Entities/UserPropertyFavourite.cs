using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HSH.Entities
{
    [Table("UserPropertyFavourite")]
    public class UserPropertyFavourite
    {
        [Key, Column(Order = 1)]
        public int PropertyId { get; set; }
        [Key, Column(Order = 2)]
        public string UserId { get; set; }
    }
}