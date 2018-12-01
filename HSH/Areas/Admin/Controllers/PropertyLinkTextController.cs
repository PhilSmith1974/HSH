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
    [Authorize(Roles = "Admin")]
    public class PropertyLinkTextController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/PropertyLinkText
        public async Task<ActionResult> Index()
        {
            return View(await db.PropertyLinkTexts.ToListAsync());
        }

        // GET: Admin/PropertyLinkText/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropertyLinkText propertyLinkText = await db.PropertyLinkTexts.FindAsync(id);
            if (propertyLinkText == null)
            {
                return HttpNotFound();
            }
            return View(propertyLinkText);
        }

        // GET: Admin/PropertyLinkText/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/PropertyLinkText/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title")] PropertyLinkText propertyLinkText)
        {
            if (ModelState.IsValid)
            {
                db.PropertyLinkTexts.Add(propertyLinkText);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(propertyLinkText);
        }

        // GET: Admin/PropertyLinkText/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropertyLinkText propertyLinkText = await db.PropertyLinkTexts.FindAsync(id);
            if (propertyLinkText == null)
            {
                return HttpNotFound();
            }
            return View(propertyLinkText);
        }

        // POST: Admin/PropertyLinkText/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title")] PropertyLinkText propertyLinkText)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propertyLinkText).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(propertyLinkText);
        }

        // GET: Admin/PropertyLinkText/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropertyLinkText propertyLinkText = await db.PropertyLinkTexts.FindAsync(id);
            if (propertyLinkText == null)
            {
                return HttpNotFound();
            }
            return View(propertyLinkText);
        }

        // POST: Admin/PropertyLinkText/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PropertyLinkText propertyLinkText = await db.PropertyLinkTexts.FindAsync(id);


            //db.PropertyLinkTexts.Remove(propertyLinkText);
            //await db.SaveChangesAsync();
            var isUnused = await db.Propertys.CountAsync(i => i.PropertyLinkTextId.Equals(id)) == 0;
            if (isUnused)
            {
                db.PropertyLinkTexts.Remove(propertyLinkText);
                await db.SaveChangesAsync();
            }




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
