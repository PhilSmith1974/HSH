using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using HSH.Areas.Admin.Models;
//using System.Collections;
using HSH.Models;
using HSH.Entities;
using System.Data.Entity;
using System.Linq;

namespace HSH.Areas.Admin.Extensions
{
    public static class ConversionExtensions
    {
        public static async Task<IEnumerable<PropertyModel>> Convert (
            this IEnumerable<Property> propertys, ApplicationDbContext db)
        {
            if (propertys.Count().Equals(0))
                return new List<PropertyModel>();

            var texts = await db.PropertyLinkTexts.ToListAsync();
            var types = await db.PropertyTypes.ToListAsync();

            return from p in propertys
                   select new PropertyModel
                   {
                       Id = p.Id,
                       Title = p.Title,
                       Description = p.Description,
                       ImageUrl = p.ImageUrl,
                       PropertyLinkTextId = p.PropertyLinkTextId,
                       PropertyTypeId = p.PropertyTypeID,
                       PropertyLinkTexts = texts,
                       PropertyTypes = types
                   };

        }
    }
}