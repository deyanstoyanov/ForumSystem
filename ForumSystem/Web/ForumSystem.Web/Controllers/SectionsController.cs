namespace ForumSystem.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.ViewModels.Section;

    public class SectionsController : BaseController
    {
        public SectionsController(IForumSystemData data)
            : base(data)
        {
        }

        // GET: Sections/Details/5
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

            var viewModel = this.Data.Sections.All()
                .Where(s => s.Id == id)
                .ProjectTo<SectionViewModel>().FirstOrDefault();

            return this.View(viewModel);
        }
    }
}