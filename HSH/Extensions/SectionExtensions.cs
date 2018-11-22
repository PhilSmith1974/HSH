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
        public static async Task<PropertySectionModel> GetPropertySectionsAsync(
            int propertyId, string userId)
        {
            var db = ApplicationDbContext.Create();

            var sections = await (
                from p in db.Propertys
                join pi in db.PropertyItems on p.Id equals pi.PropertyId
                join i in db.Items on pi.ItemId equals i.Id
                join s in db.Sections on i.SectionId equals s.Id
                where p.Id.Equals(propertyId)
                orderby s.Id
                select new PropertySection 
                {
                    Id = s.Id,
                    ItemTypeId = i.ItemTypeId,
                    Title = s.Title
                }).ToListAsync();

            foreach (var section in sections)
                section.Items = await GetPropertyItemRowsAsync(propertyId, section.Id, section.ItemTypeId, userId);

            var result = sections.Distinct(new PropertySectionEqualityComparer()).ToList();

            var union = result.Where(r => r.Title.ToLower().Contains("download"))
                .Union(result.Where(r => r.Title.ToLower().Contains("download")));

            var model = new PropertySectionModel
            {
                Sections = union.ToList(),
                Title = await (from p in db.Propertys
                               where p.Id.Equals(propertyId)
                               select p.Title).FirstOrDefaultAsync()
            };

            return model;
        }

        public static async Task<IEnumerable<PropertyItemRow>> GetPropertyItemRowsAsync(
            int propertyId, int sectionId, int itemTypeId, string userId, ApplicationDbContext db = null)
        {
            if (db == null) db = ApplicationDbContext.Create();

            var today = DateTime.Now.Date;

            var items = await(from i in db.Items
                              join it in db.ItemTypes on i.ItemTypeId equals it.Id
                              join pi in db.PropertyItems on i.Id equals pi.ItemId
                              join sp in db.FavouritePropertys on pi.PropertyId equals sp.PropertyId
                              join us in db.UserFavourites on sp.FavouriteId equals us.FavouriteId
                              where i.SectionId.Equals(sectionId) &&
                                //i.ItemTypeId.Equals(itemTypeId) &&
                                pi.PropertyId.Equals(propertyId) &&
                                us.UserId.Equals(userId)
                              orderby i.PartId
                              select new PropertyItemRow
                              {
                                  ItemId = i.Id,
                                  Description = i.Description,
                                  Title = i.Title,
                                  Link = it.Title.Equals("Download") ? i.Url : "/PropertyContent/Content/" + pi.PropertyId + "/" + i.Id,
                                  ImageUrl = i.ImageUrl,
                                  ReleaseDate = DbFunctions.CreateDateTime(us.StartDate.Value.Year,us.StartDate.Value.Month, us.StartDate.Value.Day + i.WaitDays, 0, 0, 0),
                                  IsAvailable = DbFunctions.CreateDateTime(today.Year,
                                  today.Month, today.Day, 0, 0, 0) >= DbFunctions.CreateDateTime(us.StartDate.Value.Year,
                                  us.StartDate.Value.Month, us.StartDate.Value.Day + i.WaitDays, 0, 0, 0),
                                  IsDownload = it.Title.Equals("Download")
                              }).ToListAsync();

            return items;
        }
    }
}