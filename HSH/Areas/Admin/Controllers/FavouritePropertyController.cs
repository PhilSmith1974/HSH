using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HSH.Entities;
using HSH.Models;
using HSH.Areas.Admin.Extensions;
using HSH.Areas.Admin.Models;

namespace HSH.Areas.Admin.Controllers
{
    public class FavouritePropertyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/FavouriteProperty
        public async Task<ActionResult> Index()
        {
            return View(await db.FavouritePropertys.Convert(db));
        }

        // GET: Admin/FavouriteProperty/Details/5
        public async Task<ActionResult> Details(int? favouriteId, int? propertyId)
        {
            if (favouriteId == null || propertyId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavouriteProperty favouriteProperty = await GetFavouriteProperty(favouriteId, propertyId);
            if (favouriteProperty == null)
            {
                return HttpNotFound();
            }
            return View(favouriteProperty.Convert(db));
        }

        // GET: Admin/FavouriteProperty/Create
        public async Task <ActionResult> Create()
        {
            var model = new FavouritePropertyModel
            {
               Favourites = await db.Favourites.ToListAsync(),
                Propertys = await db.Propertys.ToListAsync()

            };
            return View(model);
        }

        // POST: Admin/FavouriteProperty/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PropertyId,FavouriteId")] FavouriteProperty favouriteProperty)
        {
            if (ModelState.IsValid)
            {
                db.FavouritePropertys.Add(favouriteProperty);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(favouriteProperty);
        }

        // GET: Admin/FavouriteProperty/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavouriteProperty favouriteProperty = await db.FavouritePropertys.FindAsync(id);
            if (favouriteProperty == null)
            {
                return HttpNotFound();
            }
            return View(favouriteProperty);
        }

        // POST: Admin/FavouriteProperty/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PropertyId,FavouriteId")] FavouriteProperty favouriteProperty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(favouriteProperty).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(favouriteProperty);
        }

        // GET: Admin/FavouriteProperty/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavouriteProperty favouriteProperty = await db.FavouritePropertys.FindAsync(id);
            if (favouriteProperty == null)
            {
                return HttpNotFound();
            }
            return View(favouriteProperty);
        }

        // POST: Admin/FavouriteProperty/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FavouriteProperty favouriteProperty = await db.FavouritePropertys.FindAsync(id);
            db.FavouritePropertys.Remove(favouriteProperty);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private async Task<FavouriteProperty> GetFavouriteProperty(int? favouriteId, int? propertyId)
        {
            try
            {
                int favId = 0, prtId = 0;
                int.TryParse(favouriteId.ToString(), out favId);
                int.TryParse(propertyId.ToString(), out prtId);
                var favouriteProperty = await db.FavouritePropertys.FirstOrDefaultAsync(
                    pi => pi.PropertyId.Equals(prtId) && pi.FavouriteId.Equals(favId));
                return favouriteProperty;
            }
            catch
            {
                return null;
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
