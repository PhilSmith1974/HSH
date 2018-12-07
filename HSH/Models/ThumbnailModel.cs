using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HSH.Models
{
    public class ThumbnailModel
    {
        public int PropertyId { get; set; }
        public int FavouriteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string TagText { get; set; }
        public string ImageUrl { get; set; }
        public string Link { get; set; }
        public string ContentTag { get; set; }
        public bool IsFavourite { get; set; }
    }
}