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
    public class PropertyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Property
        public async Task<ActionResult> Index()
        {
            var propertys = await db.Propertys.ToListAsync();
            var model = await propertys.Convert(db);
            return View(model);
        }

        // GET: Admin/Property/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = await db.Propertys.FindAsync(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // GET: Admin/Property/Create
        public async Task <ActionResult> Create()
        {
            var model = new PropertyModel
            {
                PropertyLinkTexts = await db.PropertyLinkTexts.ToListAsync(),
                PropertyTypes = await db.PropertyTypes.ToListAsync()

            };
            return View();
        }

        // POST: Admin/Property/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Description,ImageUrl,PropertyLinkTextId,PropertyTypeID")] Property property)
        {
            if (ModelState.IsValid)
            {
                db.Propertys.Add(property);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(property);
        }

        // GET: Admin/Property/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = await db.Propertys.FindAsync(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            var prop = new List<Property>();
            prop.Add(property);
            var PropertyModel = await prop.Convert(db);
            return View(PropertyModel.First());
        }

        // POST: Admin/Property/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description,ImageUrl,PropertyLinkTextId,PropertyTypeID")] Property property)
        {
            if (ModelState.IsValid)
            {
                db.Entry(property).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(property);
        }

        // GET: Admin/Property/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = await db.Propertys.FindAsync(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: Admin/Property/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Property property = await db.Propertys.FindAsync(id);
            db.Propertys.Remove(property);
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
