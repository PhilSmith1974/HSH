using HSH.Areas.Admin.Models;
using HSH.Entities;
using HSH.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace HSH.Areas.Admin.Extensions
{
    #region Property
    public static class ConversionExtensions
    {

        public static async Task<IEnumerable<T>> Convert<T>(
    this IEnumerable<Property> propertys, ApplicationDbContext db) where T : PropertyModel
        {
            if (propertys.Count().Equals(0))
            {
                return new List<T>();
            }

            var texts = await db.PropertyLinkTexts.ToListAsync();
            var types = await db.PropertyTypes.ToListAsync();

            string userId = HttpContext.Current.User.Identity.GetUserId();

            return (IEnumerable<T>)from p in propertys
                                   select new PropertyModel
                                   {
                                       Id = p.Id,
                                       Title = p.Title,
                                       Description = p.Description,
                                       Price = p.Price,
                                       ImageUrl = p.ImageUrl,
                                       PropertyLinkTextId = p.PropertyLinkTextId,
                                       PropertyTypeId = p.PropertyTypeId,
                                       PropertyLinkTexts = texts,
                                       PropertyTypes = types,
                                       IsFavourite = db.UserPropertyFavourites.Any(d => d.PropertyId == p.Id && d.UserId == userId)
                                   };
        }

        public static async Task<PropertyModel> Convert(
           this Property property, ApplicationDbContext db)
        {
            var text = await db.PropertyLinkTexts.FirstOrDefaultAsync(
                p => p.Id.Equals(property.PropertyLinkTextId));
            var type = await db.PropertyTypes.FirstOrDefaultAsync(
                p => p.Id.Equals(property.PropertyTypeId));

            string userId = HttpContext.Current.User.Identity.GetUserId();

            var model = new PropertyModel
            {
                Id = property.Id,
                Title = property.Title,
                Description = property.Description,
                Price = property.Price,
                ImageUrl = property.ImageUrl,
                PropertyLinkTextId = property.PropertyLinkTextId,
                PropertyTypeId = property.PropertyTypeId,
                PropertyLinkTexts = new List<PropertyLinkText>(),
                PropertyTypes = new List<PropertyType>(),
                IsFavourite = db.UserPropertyFavourites.Any(d => d.PropertyId == property.Id && d.UserId == userId)
            };

            model.PropertyLinkTexts.Add(text);
            model.PropertyTypes.Add(type);

            return model;

        }


        public static async Task<IEnumerable<SelectListItem>> Convert(
           this IEnumerable<PropertyType> properties, ApplicationDbContext db)
        {
            return from type in properties
                   select new SelectListItem
                   {
                       Text = type.Title,
                       Value = type.Id.ToString()
                   };
        }
        #endregion

        #region Property Item
        public static async Task<IEnumerable<PropertyItemModel>> Convert(
           this IQueryable<PropertyItem> propertyItems, ApplicationDbContext db)
        {
            if (propertyItems.Count().Equals(0))
            {
                return new List<PropertyItemModel>();
            }

            return await (from pi in propertyItems
                          select new PropertyItemModel
                          {
                              ItemId = pi.ItemId,
                              PropertyId = pi.PropertyId,
                              ItemTitle = db.Items.FirstOrDefault(
                                  i => i.Id.Equals(pi.ItemId)).Title,
                              PropertyTitle = db.Propertys.FirstOrDefault(
                                 p => p.Id.Equals(pi.PropertyId)).Title

                          }).ToListAsync();
        }

        public static async Task<PropertyItemModel> Convert(
           this PropertyItem PropertyItem, ApplicationDbContext db,
           bool addListData = true)
        {
            var model = new PropertyItemModel
            {
                ItemId = PropertyItem.ItemId,
                PropertyId = PropertyItem.PropertyId,
                Items = addListData ? await db.Items.ToListAsync() : null,
                Propertys = addListData ? await db.Propertys.ToListAsync() : null,
                ItemTitle = (await db.Items.FirstOrDefaultAsync(i => i.Id.Equals(PropertyItem.ItemId))).Title,
                PropertyTitle = (await db.Propertys.FirstOrDefaultAsync(p =>
               p.Id.Equals(PropertyItem.PropertyId))).Title
            };

            return model;

        }

        public static async Task<bool> CanChange(this PropertyItem propertyItem, ApplicationDbContext db)
        {
            var oldPI = await db.PropertyItems.CountAsync(pi =>
                    pi.PropertyId.Equals(propertyItem.OldPropertyId) &&
                    pi.ItemId.Equals(propertyItem.OldItemId));

            var newPI = await db.PropertyItems.CountAsync(pi =>
            pi.PropertyId.Equals(propertyItem.PropertyId) &&
            pi.ItemId.Equals(propertyItem.ItemId));

            return oldPI.Equals(1) && newPI.Equals(0);
        }

        public static async Task Change(
            this PropertyItem propertyItem, ApplicationDbContext db)
        {
            var oldPropertyItem = await db.PropertyItems.FirstOrDefaultAsync(
                pi => pi.PropertyId.Equals(propertyItem.OldPropertyId) &&
                pi.ItemId.Equals(propertyItem.OldItemId));

            var newPropertyItem = await db.PropertyItems.FirstOrDefaultAsync(
                pi => pi.PropertyId.Equals(propertyItem.PropertyId) &&
                pi.ItemId.Equals(propertyItem.ItemId));

            if (oldPropertyItem != null && newPropertyItem == null)
            {
                newPropertyItem = new PropertyItem
                {
                    ItemId = propertyItem.ItemId,
                    PropertyId = propertyItem.PropertyId
                };

                using (var transaction = new TransactionScope(
                    TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        db.PropertyItems.Remove(oldPropertyItem);
                        db.PropertyItems.Add(newPropertyItem);

                        await db.SaveChangesAsync();
                        transaction.Complete();
                    }
                    catch { transaction.Dispose(); }

                }
            }
        }
        #endregion

        #region Favourite Property
        public static async Task<IEnumerable<FavouritePropertyModel>> Convert(
          this IQueryable<FavouriteProperty> favouritePropertys, ApplicationDbContext db)
        {
            if (favouritePropertys.Count().Equals(0))
            {
                return new List<FavouritePropertyModel>();
            }

            return await (from pi in favouritePropertys
                          select new FavouritePropertyModel
                          {
                              FavouriteId = pi.FavouriteId,
                              PropertyId = pi.PropertyId,
                              FavouriteTitle = db.Favourites.FirstOrDefault(
                                  i => i.Id.Equals(pi.FavouriteId)).Title,
                              PropertyTitle = db.Propertys.FirstOrDefault(
                                 p => p.Id.Equals(pi.PropertyId)).Title

                          }).ToListAsync();
        }

        public static async Task<FavouritePropertyModel> Convert(
           this FavouriteProperty favouriteProperty,
           ApplicationDbContext db, bool addListData = true)
        {
            var model = new FavouritePropertyModel
            {
                FavouriteId = favouriteProperty.FavouriteId,
                PropertyId = favouriteProperty.PropertyId,
                Favourites = addListData ? await db.Favourites.ToListAsync() : null,
                Propertys = addListData ? await db.Propertys.ToListAsync() : null,
                FavouriteTitle = (await db.Favourites.FirstOrDefaultAsync(
                    f => f.Id.Equals(favouriteProperty.FavouriteId))).Title,
                PropertyTitle = (await db.Propertys.FirstOrDefaultAsync(p =>
               p.Id.Equals(favouriteProperty.PropertyId))).Title
            };

            return model;

        }

        public static async Task<bool> CanChange(
            this FavouriteProperty favouriteProperty,
            ApplicationDbContext db)
        {
            var oldFP = await db.FavouritePropertys.CountAsync(fp =>
                    fp.PropertyId.Equals(favouriteProperty.OldPropertyId) &&
                    fp.FavouriteId.Equals(favouriteProperty.OldFavouriteId));

            var newFP = await db.FavouritePropertys.CountAsync(fp =>
            fp.PropertyId.Equals(favouriteProperty.PropertyId) &&
            fp.FavouriteId.Equals(favouriteProperty.FavouriteId));

            return oldFP.Equals(1) && newFP.Equals(0);
        }

        public static async Task Change(
            this FavouriteProperty favouriteProperty, ApplicationDbContext db)
        {
            var oldFavouriteProperty = await db.FavouritePropertys.FirstOrDefaultAsync(
                pi => pi.PropertyId.Equals(favouriteProperty.OldPropertyId) &&
                pi.FavouriteId.Equals(favouriteProperty.OldFavouriteId));

            var newFavouriteProperty = await db.FavouritePropertys.FirstOrDefaultAsync(
                pi => pi.PropertyId.Equals(favouriteProperty.PropertyId) &&
                pi.FavouriteId.Equals(favouriteProperty.FavouriteId));

            if (oldFavouriteProperty != null && newFavouriteProperty == null)
            {
                newFavouriteProperty = new FavouriteProperty
                {
                    FavouriteId = favouriteProperty.FavouriteId,
                    PropertyId = favouriteProperty.PropertyId
                };

                using (var transaction = new TransactionScope(
                    TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        db.FavouritePropertys.Remove(oldFavouriteProperty);
                        db.FavouritePropertys.Add(newFavouriteProperty);

                        await db.SaveChangesAsync();
                        transaction.Complete();
                    }
                    catch { transaction.Dispose(); }

                }
            }
        }

        #endregion

    }
}
