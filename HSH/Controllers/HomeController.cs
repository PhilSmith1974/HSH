using HSH.Models;
using HSH.Areas.Admin.Extensions;
using HSH.Areas.Admin.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using HSH.Extensions;


namespace HSH.Controllers
{
    public class HomeController : Controller
    {

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

            return View(model);
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


        private ApplicationDbContext db = new ApplicationDbContext();
        // Search Property

        public ActionResult Search()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<ActionResult> SearchIndex(PropertySearchModel searchModel)
        {
            var result = db.Propertys.AsQueryable();
            if (searchModel != null)
            {
                //Keyword
                if (!string.IsNullOrEmpty(searchModel.TitleKeyword))
                {
                    result = result.Where(t => t.Title.Contains(searchModel.TitleKeyword));
                }

                //Price 
                if (searchModel.Price.HasValue)
                {
                    result = result.Where(t => t.Price <= searchModel.Price);
                }
                //Property Type (Int to String.....)
                //if (!string.IsNullOrEmpty(searchModel.PropertyType))
                //{
                //    result = result.Where(t => t.PropertyType.Model.Equals(searchModel.PropertyType));
                //}

                //Property Type 
                if (searchModel.PropertyType.HasValue)
                {
                    result = result.Where(t => t.PropertyTypeId <= searchModel.PropertyType);
                }

                //Search on county (address issue)
                //if (!string.IsNullOrEmpty(searchModel.County))
                //{
                //    result = result.Where(t => t.County == searchModel.County);
                //}
                // Number of Bedrooms
                if (searchModel.NumberOfBedrooms.HasValue)
                {
                    result = result.Where(t => t.NumberOfBedrooms >= searchModel.NumberOfBedrooms);
                }


                //if (searchModel.MinManufacturerYear.HasValue)
                //{
                //    result = result.Where(t => t.ManufacturerYear >= searchModel.MinManufacturerYear);
                //}
                //if (searchModel.MaxManufacturerYear.HasValue)
                //{
                //    result = result.Where(t => t.ManufacturerYear <= searchModel.MaxManufacturerYear);
                //}

            }
            var Propertys = await result.ToListAsync();
            var model = await Propertys.Convert(db);
            //return View(model);
            return View(model.OrderBy(t => t.Price));
        }
    }
}