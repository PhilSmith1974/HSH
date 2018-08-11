using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HSH.Entities
{
    [Table ("PropertyItem")]
    public class PropertyItem
    {
        [Required]
        [Key,Column(Order =1)]
        public int PropertyId { get; set; }

        [Required]
        [Key, Column(Order = 2)]
        public int ItemId { get; set; }

        public int FavouriteId { get; set; }

        [NotMapped]
        public int OldPropertyId { get; set; }

        [NotMapped]
        public int OldItemId { get; set; }


    }
}