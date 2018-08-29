using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using HSH.Areas.Admin.Models;
using System.Collections;
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
                       Price =p.Price,
                       ImageUrl = p.ImageUrl,
                       PropertyLinkTextId = p.PropertyLinkTextId,
                       PropertyTypeId = p.PropertyTypeID,
                       PropertyLinkTexts = texts,
                       PropertyTypes = types
                   };

        }

        public static async Task<PropertyModel> Convert(
           this Property property, ApplicationDbContext db)
        {
            var text = await db.PropertyLinkTexts.FirstOrDefaultAsync(
                p => p.Id.Equals(property.PropertyLinkTextId));
            var type = await db.PropertyTypes.FirstOrDefaultAsync(
                p => p.Id.Equals(property.PropertyTypeID));

            
            var model = new PropertyModel
            {
                       Id = property.Id,
                       Title = property.Title,
                       Description = property.Description,
                       Price = property.Price,
                       ImageUrl = property.ImageUrl,
                       PropertyLinkTextId = property.PropertyLinkTextId,
                       PropertyTypeId = property.PropertyTypeID,
                       PropertyLinkTexts = new List <PropertyLinkText>(),
                       PropertyTypes = new List <PropertyType>()
                   };

            model.PropertyLinkTexts.Add(text);
            model.PropertyTypes.Add(type);

            return model;

        }
//#endregion

//        #region Product Item
//        public static async Task<IEnumerable<PropertyItemModel>> Convert(
//        this IQueryable<PropertyItem> productItems, ApplicationDbContext db)
//        {
//            if (productItems.Count().Equals(0))
//                return new List<PropertyItemModel>();

//            return await (from pi in productItems
//                          select new PropertyItemModel
//                          {
//                              ItemId = pi.ItemId,
//                              ProductId = pi.PropertyId,
//                              ItemTitle = db.Items.FirstOrDefault(
//                                  i => i.Id.Equals(pi.ItemId)).Title,
//                              ProductTitle = db.Propertys.FirstOrDefault(
//                                  p => p.Id.Equals(pi.PropertyId)).Title
//                          }).ToListAsync();
//        }

//        public static async Task<PropertyItemModel> Convert(
//        this PropertyItem productItem, ApplicationDbContext db,
//        bool addListData = true)
//        {
//            var model = new PropertyItemModel
//            {
//                ItemId = productItem.ItemId,
//                ProductId = productItem.PropertyId,
//                Items = addListData ? await db.Items.ToListAsync() : null,
//                Products = addListData ? await db.Propertys.ToListAsync() : null,
//                ItemTitle = (await db.Items.FirstOrDefaultAsync(i =>
//                   i.Id.Equals(productItem.ItemId))).Title,
//                ProductTitle = (await db.Propertys.FirstOrDefaultAsync(p =>
//                   p.Id.Equals(productItem.PropertyId))).Title
//            };

//            return model;
//        }

//        public static async Task<bool> CanChange(
//            this PropertyItem productItem, ApplicationDbContext db)
//        {
//            var oldPI = await db.PropertyItems.CountAsync(pi =>
//                pi.PropertyId.Equals(productItem.OldPropertyId) &&
//                pi.ItemId.Equals(productItem.OldItemId));

//            var newPI = await db.PropertyItems.CountAsync(pi =>
//                pi.PropertyId.Equals(productItem.PropertyId) &&
//                pi.ItemId.Equals(productItem.ItemId));

//            return oldPI.Equals(1) && newPI.Equals(0);
//        }

//        public static async Task Change(
//            this PropertyItem productItem, ApplicationDbContext db)
//        {
//            var oldProductItem = await db.PropertyItems.FirstOrDefaultAsync(
//                    pi => pi.PropertyId.Equals(productItem.OldPropertyId) &&
//                    pi.ItemId.Equals(productItem.OldItemId));

//            var newProductItem = await db.PropertyItems.FirstOrDefaultAsync(
//                pi => pi.PropertyId.Equals(productItem.PropertyId) &&
//                pi.ItemId.Equals(productItem.ItemId));

//            if (oldProductItem != null && newProductItem == null)
//            {
//                newProductItem = new PropertyItem
//                {
//                    ItemId = productItem.ItemId,
//                    PropertyId = productItem.PropertyId
//                };

                //using (var transaction = new TransactionScope(
                //    TransactionScopeAsyncFlowOption.Enabled))
                //{
                //    try
                //    {
                //        db.PropertyItems.Remove(oldProductItem);
                //        db.PropertyItems.Add(newProductItem);

                //        await db.SaveChangesAsync();
                //        transaction.Complete();
                //    }
                //    catch { transaction.Dispose(); }
                //}
            }
        }
        //#endregion

        //#region Subscription Product
        //public static async Task<IEnumerable<SubscriptionProductModel>> Convert(
        //this IQueryable<SubscriptionProduct> subscriptionProducts, ApplicationDbContext db)
        //{
        //    if (subscriptionProducts.Count().Equals(0))
        //        return new List<SubscriptionProductModel>();

        //    return await (from pi in subscriptionProducts
        //                  select new SubscriptionProductModel
        //                  {
        //                      SubscriptionId = pi.SubscriptionId,
        //                      ProductId = pi.ProductId,
        //                      SubscriptionTitle = db.Subscriptions.FirstOrDefault(
        //                          i => i.Id.Equals(pi.SubscriptionId)).Title,
        //                      ProductTitle = db.Products.FirstOrDefault(
        //                          p => p.Id.Equals(pi.ProductId)).Title
        //                  }).ToListAsync();
        //}

        //public static async Task<SubscriptionProductModel> Convert(
        //this SubscriptionProduct subscriptionProduct,
        //ApplicationDbContext db, bool addListData = true)
        //{
        //    var model = new SubscriptionProductModel
        //    {
        //        SubscriptionId = subscriptionProduct.SubscriptionId,
        //        ProductId = subscriptionProduct.ProductId,
        //        Subscriptions = addListData ? await db.Subscriptions.ToListAsync() : null,
        //        Products = addListData ? await db.Products.ToListAsync() : null,
        //        SubscriptionTitle = (await db.Subscriptions.FirstOrDefaultAsync(s =>
        //           s.Id.Equals(subscriptionProduct.SubscriptionId))).Title,
        //        ProductTitle = (await db.Products.FirstOrDefaultAsync(p =>
        //           p.Id.Equals(subscriptionProduct.ProductId))).Title
        //    };

        //    return model;
        //}

        //public static async Task<bool> CanChange(
        //    this SubscriptionProduct subscriptionProduct,
        //    ApplicationDbContext db)
        //{
        //    var oldSP = await db.SubscriptionProducts.CountAsync(sp =>
        //        sp.ProductId.Equals(subscriptionProduct.OldProductId) &&
        //        sp.SubscriptionId.Equals(subscriptionProduct.OldSubscriptionId));

        //    var newSP = await db.SubscriptionProducts.CountAsync(sp =>
        //        sp.ProductId.Equals(subscriptionProduct.ProductId) &&
        //        sp.SubscriptionId.Equals(subscriptionProduct.SubscriptionId));

        //    return oldSP.Equals(1) && newSP.Equals(0);
        //}

        //public static async Task Change(
        //    this SubscriptionProduct subscriptionProduct,
        //    ApplicationDbContext db)
        //{
        //    var oldSubscriptionProduct = await db.SubscriptionProducts.FirstOrDefaultAsync(
        //            sp => sp.ProductId.Equals(subscriptionProduct.OldProductId) &&
        //            sp.SubscriptionId.Equals(subscriptionProduct.OldSubscriptionId));

        //    var newSubscriptionProduct = await db.SubscriptionProducts.FirstOrDefaultAsync(
        //        sp => sp.ProductId.Equals(subscriptionProduct.ProductId) &&
        //        sp.SubscriptionId.Equals(subscriptionProduct.SubscriptionId));

        //    if (oldSubscriptionProduct != null && newSubscriptionProduct == null)
        //    {
        //        newSubscriptionProduct = new SubscriptionProduct
        //        {
        //            SubscriptionId = subscriptionProduct.SubscriptionId,
        //            ProductId = subscriptionProduct.ProductId
        //        };

        //        using (var transaction = new TransactionScope(
        //            TransactionScopeAsyncFlowOption.Enabled))
        //        {
        //            try
        //            {
        //                db.SubscriptionProducts.Remove(oldSubscriptionProduct);
        //                db.SubscriptionProducts.Add(newSubscriptionProduct);

        //                await db.SaveChangesAsync();
        //                transaction.Complete();
        //            }
        //            catch { transaction.Dispose(); }
        //        }
        //    }
        //}
        //#endregion
