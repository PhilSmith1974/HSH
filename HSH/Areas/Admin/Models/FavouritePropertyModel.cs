using HSH.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HSH.Areas.Admin.Models
{
    public class FavouritePropertyModel
    {
        [DisplayName("Property Id")]
        public int PropertyId { get; set; }
        [DisplayName("Favourite Id")]
        public int FavouriteId { get; set; }
        [DisplayName("Property Title")]
        public string PropertyTitle { get; set; }
        [DisplayName("Favourite Title")]
        public string FavouriteTitle { get; set; }
        public ICollection<Property>Propertys { get; set; }
        public ICollection<Favourite> Favourites { get; set; }

    }
}