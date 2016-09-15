namespace ForumSystem.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Controllers.Base;
    using ForumSystem.Web.ViewModels.Categories;
    using ForumSystem.Web.ViewModels.Sections;

    public class SectionsController : BaseController
    {
        public SectionsController(IForumSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var section = this.Data.Sections.GetById(id);
            if (section == null)
            {
                return this.HttpNotFound();
            }

            var viewModel =
                this.Data.Sections.All()
                    .Where(s => s.Id == id)
                    .ProjectTo<SectionViewModel>().FirstOrDefault();

            return this.View(viewModel);
        }

        [ChildActionOnly]
        public ActionResult Categories(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var section = this.Data.Sections.GetById(id);
            if (section == null || section.IsDeleted)
            {
                return this.HttpNotFound();
            }

            var categories =
                this.Data.Categories.All()
                    .Where(c => c.SectionId == id)
                    .OrderByDescending(c => c.CreatedOn)
                    .ProjectTo<CategoryConciseViewModel>()
                    .ToList();

            return this.PartialView("_SectionCategoriesPartial", categories);
        }
    }
}