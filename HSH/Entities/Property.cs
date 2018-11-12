using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HSH.Entities
{
    [Table("Property")]

    public class Property
    {
        //Not Nullable
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(255)]
        [Required]
        public string Title { get; set; }
        [MaxLength(2048)]

        public string Description { get; set; }



        [Required]
        public double Price { get; set; }

        [MaxLength(1024)]
        public string ImageUrl { get; set; }
        public int PropertyLinkTextId { get; set; }
        public int PropertyTypeID { get; set; }
      
    }
}