using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ForumSystem.Data;
using ForumSystem.Data.Models;

namespace ForumSystem.Web.Controllers
{
    using ForumSystem.Data.UnitOfWork;

    public class CategoriesController : BaseController
    {
        public CategoriesController(IForumSystemData data)
         : base(data)
        {
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = this.Data.Categories.GetById(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }
    }
}
