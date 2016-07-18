namespace ForumSystem.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.ViewModels.Category;

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
                return this.HttpNotFound();
            }

            var viewModel = this.Data.Categories.All()
                .Where(c => c.Id == id)
                .ProjectTo<CategoryViewModel>()
                .FirstOrDefault();

            return this.View(viewModel);
        }
    }
}