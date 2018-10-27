using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace HSH.Areas.Admin.Models
{
    public class EditButtonModel
    {
        public int ItemId { get; set; }
        public int PropertyId { get; set; }
        public int FavouriteId { get; set; }
        public string Link
        {
            get
            {
                var s = new StringBuilder("?");
                if (ItemId > 0) s.Append(String.Format("{0}={1}&", "itemId", ItemId));
                if (PropertyId > 0) s.Append(String.Format("{0}={1}&", "propertyId", PropertyId));
                if (FavouriteId > 0) s.Append(String.Format("{0}={1}&", "favouriteId", FavouriteId));
                return s.ToString().Substring(0, s.Length - 1);

            }
        }
    }
}