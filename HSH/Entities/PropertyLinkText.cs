using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HSH.Entities
{
    [Table("PropertyLinkText")]

    public class PropertyLinkText
    {
        //Not Nullable
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(25)]
        [Required]

        public string Title { get; set; }
    }
}