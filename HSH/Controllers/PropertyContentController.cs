using HSH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HSH.Controllers
{
    public class PropertyContentController : Controller
    {
        [Authorize]
        // GET: ProductContent
        public async Task <ActionResult> Index(int id)
        {
            var model = new PropertySectionModel
            {
                Title = "The Title",
                Sections = new List<PropertySection>()
            };
            return View(model);
        }
    }
}