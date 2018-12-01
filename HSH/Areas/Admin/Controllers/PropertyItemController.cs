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
using HSH.Areas.Admin.Models;
using HSH.Areas.Admin.Extensions;

namespace HSH.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Agent")]
    public class PropertyItemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/PropertyItem
        public async Task<ActionResult> Index()
        {
            return View(await db.PropertyItems.Convert(db));
        }

        // GET: Admin/PropertyItem/Details/5
        public async Task<ActionResult> Details(int? itemId, int? propertyId)
        {
            if (itemId == null || propertyId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropertyItem propertyItem = await GetPropertyItem(itemId, propertyId);
            if (propertyItem == null)
            {
                return HttpNotFound();
            }
            return View(await propertyItem.Convert(db));
        }

        // GET: Admin/PropertyItem/Create
        public async Task <ActionResult> Create()
        {
            var model = new PropertyItemModel
            {
                Items = await db.Items.ToListAsync(),
                Propertys = await db.Propertys.ToListAsync()

            };
            return View(model);
        }

        // POST: Admin/PropertyItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PropertyId,ItemId")] PropertyItem propertyItem)
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
        public async Task<ActionResult> Edit(int? itemId, int? propertyId )
        {
            if (itemId == null || propertyId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropertyItem propertyItem = await GetPropertyItem(itemId, propertyId);
            if (propertyItem == null)
            {
                return HttpNotFound();
            }
            return View(await propertyItem.Convert(db));
        }

        // POST: Admin/PropertyItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PropertyId,ItemId,OldPropertyId,OldItemId")]
        PropertyItem propertyItem)
        {
            if (ModelState.IsValid)
            {
                var canChange = await propertyItem.CanChange(db);
                if (canChange)
                    await propertyItem.Change(db);
                return RedirectToAction("Index");
            }
            return View(propertyItem);
        }

        // GET: Admin/PropertyItem/Delete/5
        public async Task<ActionResult> Delete(int? itemId, int? propertyId)
        {
            if (itemId == null || propertyId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropertyItem propertyItem = await GetPropertyItem(itemId, propertyId);
            if (propertyItem == null)
            {
                return HttpNotFound();
            }
            return View(await propertyItem.Convert(db));
        }

        // POST: Admin/PropertyItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(
            int itemId, int propertyId)
        {
            PropertyItem propertyItem = await GetPropertyItem(itemId,propertyId);
            db.PropertyItems.Remove(propertyItem);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        private async Task<PropertyItem> GetPropertyItem (int? itemId,int? propertyId)
        {
            try
            {
                int itmId = 0, prtId =0;
                int.TryParse(itemId.ToString(), out itmId);
                int.TryParse(propertyId.ToString(), out prtId);
                var propertyItem = await db.PropertyItems.FirstOrDefaultAsync(
                    pi => pi.PropertyId.Equals(prtId) && pi.ItemId.Equals(itmId));
                return propertyItem;
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
