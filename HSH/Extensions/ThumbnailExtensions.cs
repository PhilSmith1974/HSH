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
    public static class ThumbnailExtensions
    {

        private static async Task<List<int>> GetFavouriteIdsAsync(
            string userId = null, ApplicationDbContext db = null)
        {
            try
            {
                if (userId == null) return new List<int>();
                if (db == null) db = ApplicationDbContext.Create();

                return await (
                    from us in db.UserFavourites
                    where us.UserId.Equals(userId)
                    select us.FavouriteId).ToListAsync();
            }
            catch { }
            return new List<int>();
        }

        public static async Task<IEnumerable<ThumbnailModel>> GetPropertyThumbnailsAsync(
    this List<ThumbnailModel> thumbnails, string userId = null,
    ApplicationDbContext db = null)
        {
            try
            {
                if (userId == null) return new List<ThumbnailModel>();
                if (db == null) db = ApplicationDbContext.Create();

                var favouriteIds = await GetFavouriteIdsAsync(userId, db);

                thumbnails = await (
                    from pf in db.FavouritePropertys
                    join p in db.Propertys on pf.PropertyId equals p.Id
                    join plt in db.PropertyLinkTexts on p.PropertyLinkTextId equals plt.Id
                    join pt in db.PropertyTypes on p.PropertyTypeId equals pt.Id
                    where favouriteIds.Contains(pf.FavouriteId)
                    select new ThumbnailModel
                    {
                        PropertyId = p.Id,
                        FavouriteId = pf.FavouriteId,
                        Title = p.Title,
                        Description = p.Description,
                        ImageUrl = p.ImageUrl,
                        Price = p.Price,
                        Link = "/PropertyContent/Index/" + p.Id,
                        TagText = plt.Title,
                        ContentTag = pt.Title
                    }).ToListAsync();
            }
            catch { }
            return thumbnails.Distinct(new ThumbnailEqualityComparer()).OrderBy(o => o.Title);
        }
    }
}