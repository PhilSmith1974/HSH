using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HSH.Areas.Admin.Models
{
    public class SmallButtonModel
    {
        public string Action { get; set; }
        public string Text { get; set; }
        public string Glych { get; set; }
        public string ButtonType { get; set; }
        public int? Id { get; set; }
        public int? ItemId { get; set; }
        public int? PropertyId { get; set; }
        public string UserId { get; set; }
        public int? FavouriteId { get; set; }
        public string ActionParameters {
            get {
                var param = new StringBuilder("?");
                if(Id !=null && Id > 0)
                    param.Append(String.Format("{0}={1}&","id", Id));

                if (Id != null && ItemId > 0)
                    param.Append(String.Format("{0}={1}&","itemId", ItemId));

                if (Id != null && PropertyId > 0)
                    param.Append(String.Format("{0}={1}&", "propertyId", PropertyId));

                if (Id != null && FavouriteId > 0)
                    param.Append(String.Format("{0}={1}&", "favouriteId", FavouriteId));

                if (UserId != null && !UserId.Equals(string.Empty))
                    param.Append(string.Format("{0}={1}&", "UserId", UserId));

                return param.ToString().Substring(0, param.Length - 1);
                }
        }
    }
}