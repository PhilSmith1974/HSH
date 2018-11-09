using HSH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HSH.Comparers
{
    public class ThumbnailEqualityComparer : IEqualityComparer<ThumbnailModel>
    {
        public bool Equals(ThumbnailModel thumb1, ThumbnailModel thumb2)
        {
            return thumb1.PropertyId.Equals(thumb2.PropertyId);
        }

        public int GetHashCode(ThumbnailModel thumb)
        {
            return thumb.PropertyId;
        }
    }
}