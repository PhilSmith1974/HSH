using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HSH.Entities
{
    [Table ("FavouriteProperty")]
    public class FavouriteProperty
    {
        [Required]
        [Key,Column(Order =1)]
        public int PropertyId { get; set; }

        [Required]
        [Key, Column(Order = 2)]
        public int FavouriteId { get; set; }

        
        [NotMapped]
        public int OldFavouriteId { get; set; }

        [NotMapped]
        public int OldItemId { get; set; }


    }
}