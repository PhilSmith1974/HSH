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

namespace HSH.Areas.Admin.Controllers
{
    public class PropertyItemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/PropertyItem
        public async Task<ActionResult> Index()
        {
            return View(await db.PropertyItems.ToListAsync());
        }

        // GET: Admin/PropertyItem/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropertyItem propertyItem = await db.PropertyItems.FindAsync(id);
            if (propertyItem == null)
            {
                return HttpNotFound();
            }
            return View(propertyItem);
        }

        // GET: Admin/PropertyItem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/PropertyItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PropertyId,ItemId,FavouriteId")] PropertyItem propertyItem)
        {
            if (ModelState.IsValid)
            {
                db.PropertyItems.Add(propertyItem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(propertyItem);
        }

        // GET: Admin/PropertyItem/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropertyItem propertyItem = await db.PropertyItems.FindAsync(id);
            if (propertyItem == null)
            {
                return HttpNotFound();
            }
            return View(propertyItem);
        }

        // POST: Admin/PropertyItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PropertyId,ItemId,FavouriteId")] PropertyItem propertyItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propertyItem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(propertyItem);
        }

        // GET: Admin/PropertyItem/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropertyItem propertyItem = await db.PropertyItems.FindAsync(id);
            if (propertyItem == null)
            {
                return HttpNotFound();
            }
            return View(propertyItem);
        }

        // POST: Admin/PropertyItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PropertyItem propertyItem = await db.PropertyItems.FindAsync(id);
            db.PropertyItems.Remove(propertyItem);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
