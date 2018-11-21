using HSH.Extensions;
using HSH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HSH.Controllers
{
    [Authorize]
    public class PropertyContentController : Controller
    {
       
        // GET: ProductContent
        public async Task <ActionResult> Index(int id)
        {  
            var userId = Request.IsAuthenticated ? HttpContext.GetUserId() : null;
            var sections = await SectionExtensions.GetPropertySectionsAsync(id, userId);
            return View(sections);
        }
    }
}