using HSH.Comparers;
using HSH.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HSH.Extensions
{
    public static class SectionExtensions
    {
        public static async Task<PropertySectionModel> GetPropertySectionsAsync(int propertyId, string userId)
        {
            var db = ApplicationDbContext.Create();

            var sections = await (
                from p in db.Propertys
                join pi in db.PropertyItems on p.Id equals pi.PropertyId
                join i in db.Items on pi.ItemId equals i.Id
                join s in db.Sections on i.SectionId equals s.Id
                where p.Id.Equals(propertyId)
                orderby s.Title
                select new PropertySection
                {
                    Id = s.Id,
                    ItemTypeId = i.ItemTypeId,
                    Title = s.Title
                }).ToListAsync();

            var result = sections.Distinct(new PropertySectionEqualityComparer()).ToList();

            var model = new PropertySectionModel
            {
                Sections = result,
                Title = await (from p in db.Propertys
                               where p.Id.Equals(propertyId)
                               select p.Title).FirstOrDefaultAsync()
            };

            return model;
        }
    }
}