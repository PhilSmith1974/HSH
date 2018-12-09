using HSH.Areas.Admin.Extensions;
using HSH.Areas.Admin.Models;
using HSH.Entities;
using HSH.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
//using System.Transactions;

namespace HSH.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Agent")]
    public class PropertyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Property
        public async Task<ActionResult> Index()
        {
            var propertys = await db.Propertys.ToListAsync();
            var model = await propertys.Convert<PropertyModel>(db);
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
            var model = await property.Convert(db);
            return View(model);
        }

        // GET: Admin/Property/Create
        public async Task<ActionResult> Create()
        {
            var model = new PropertyModel
            {
                PropertyLinkTexts = await db.PropertyLinkTexts.ToListAsync(),
                PropertyTypes = await db.PropertyTypes.ToListAsync()

            };
            return View(model);
        }

        // POST: Admin/Property/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PropertyModel model)
        {
          
            if (ModelState.IsValid)
            {
                Property prop = new Property();
                prop.Address = new Address();
                prop.Description = model.Description;
                prop.ImageUrl = model.ImageUrl;
                prop.NumberOfBedrooms = model.NumberOfBedrooms;
                prop.Price = model.Price;
                prop.PropertyLinkTextId = model.PropertyLinkTextId;
                prop.PropertyTypeId = model.PropertyTypeId;
                prop.Title = model.Title;

                prop.Address.County = model.County;
                prop.Address.DependentLocality = model.DependentLocality;
                prop.Address.DoubleDependentLocality = model.DoubleDependentLocality;
                prop.Address.Number = model.Number;
                prop.Address.PostCode = model.PostCode;
                prop.Address.PostTown = model.PostTown;
                prop.Address.Premise = model.Premise;
                prop.Address.Street = model.Street;
                prop.Address.SummaryLine = model.SummaryLine;

                db.Propertys.Add(prop);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
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
            var PropertyModel = await prop.Convert<PropertyModel>(db);
            return View(PropertyModel.First());
        }

        // POST: Admin/Property/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description,Price,ImageUrl,PropertyLinkTextId,PropertyTypeID")] Property property)
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
            var model = await property.Convert(db);
            return View(model);

        }

        // POST: Admin/Property/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Property property = await db.Propertys.FindAsync(id);


            //db.Propertys.Remove(property);
            //await db.SaveChangesAsync();
            using (var transaction = new TransactionScope(
                        TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var propItems = db.PropertyItems.Where(
                        pi => pi.PropertyId.Equals(id));
                    var propFav = db.FavouritePropertys.Where(
                        sp => sp.PropertyId.Equals(id));
                    db.PropertyItems.RemoveRange(propItems);
                    db.FavouritePropertys.RemoveRange(propFav);
                    db.Propertys.Remove(property);

                    await db.SaveChangesAsync();
                    transaction.Complete();
                }
                catch { transaction.Dispose(); }
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

