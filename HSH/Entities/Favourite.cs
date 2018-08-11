using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HSH.Entities
{
    [Table("Favourite")]

    public class Favourite
    {
        //Not Nullable
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(255)]
        [Required]
        public string Title { get; set; }
        [MaxLength(255)]

        public string Description { get; set; }
        [MaxLength(255)]

        public string RegisterationCode { get; set; }


    }
}