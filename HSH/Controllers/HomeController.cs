using HSH.Areas.Admin.Extensions;
using HSH.Areas.Admin.Models;
using HSH.Entities;
using HSH.Extensions;
using HSH.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace HSH.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        ////Added for unit Testing
        //public HomeController() { }

        //added for unit testing
        //public HomeController(IApplicationDbContext context)
        //{
        //    db = context;
        //}

        public async Task<ActionResult> Index()
        {
            var userId = Request.IsAuthenticated ? HttpContext.User.Identity.GetUserId() : null;
            var thumbnails = await new List<ThumbnailModel>().GetPropertyThumbnailsAsync(userId);

            var count = thumbnails.Count() / 4;
            var model = new List<ThumbnailAreaModel>();
            for (int i = 0; i <= count; i++)
            {
                model.Add(new ThumbnailAreaModel
                {
                    Title = i.Equals(0) ? "My Content" : string.Empty,
                    Thumbnails = thumbnails.Skip(i * 4).Take(4)
                });
            }

            return View("Index", model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public async Task<ActionResult> Search()
        {
            var model = new PropertySearchModel();

            model.PropertyTypesList = (await db.PropertyTypes.Convert(db)).ToList();
            model.PropertyTypesList.Add(new SelectListItem() { Text = "", Value = null, Selected = true });

            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> SearchIndex(PropertySearchModel searchModel)
        {
            if (searchModel != null)
            {
                var result = from prop in db.Propertys
                             where
                             ((prop.Title.Contains(searchModel.SearchTerm) || prop.Description.Contains(searchModel.SearchTerm)) || string.IsNullOrEmpty(searchModel.SearchTerm)) &&
                             (prop.Address.County.Contains(searchModel.County) || string.IsNullOrEmpty(searchModel.County)) &&
                             (prop.Price >= (searchModel.PriceFrom ?? double.MinValue) && prop.Price <= (searchModel.PriceTo ?? double.MaxValue)) &&
                             ((prop.NumberOfBedrooms >= (searchModel.NumberOfBedroomsFrom ?? int.MinValue)) && (prop.NumberOfBedrooms <= (searchModel.NumberOfBedroomsTo ?? int.MaxValue))) &&
                             (prop.PropertyTypeId == (searchModel.PropertyTypeId ?? prop.PropertyTypeId))
                             select prop;

                var properties = await result.ToListAsync();
                var model = await properties.Convert<PropertyModel>(db);
                return View(model.OrderBy(t => t.Price));
            }

            return View();
        }

        public async Task<ActionResult> AddToFavourites(int propertyId)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var property = db.Propertys.FirstOrDefault(d => d.Id == propertyId);

            if (property == null)
            {
                throw new ApplicationException("Property Not Found");
            }

            string userId = HttpContext.GetUserId();

            try
            {
                db.UserPropertyFavourites.Add(new UserPropertyFavourite() { PropertyId = propertyId, UserId = userId });
                db.SaveChanges();
            }
            catch (Exception) { }

            return RedirectToAction("Index", "Home");
            ;
        }


        public async Task<ActionResult> RemoveFromFavourites(int propertyId)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var property = db.Propertys.FirstOrDefault(d => d.Id == propertyId);

            if (property == null)
            {
                throw new ApplicationException("Property Not Found");
            }

            string userId = HttpContext.GetUserId();

            var favourite = db.UserPropertyFavourites.FirstOrDefault(f => f.PropertyId == propertyId && f.UserId == userId);

            try
            {
                if (favourite != null)
                {
                    db.UserPropertyFavourites.Remove(favourite);
                    db.SaveChanges();
                }
            }
            catch (Exception) { }

            return RedirectToAction("Index", "Home");
        }
    }
}