using HSH.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HSH.Models
{
    public class UserFavouriteViewModel
    {
        public ICollection<Favourite> Favourites  { get; set; }
        public ICollection <UserFavouriteModel> UserFavourites{ get; set; }

        public bool DisableDropDown { get; set; }
        public string UserId { get; set; }
        public int FavouriteId { get; set; }
       
    }
}