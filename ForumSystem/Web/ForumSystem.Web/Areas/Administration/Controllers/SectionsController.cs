namespace ForumSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Areas.Administration.ViewModels.Sections;

    public class SectionsController : AdministrationController
    {
        public SectionsController(IForumSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult All()
        {
            var sections =
                this.Data.Sections.AllWithDeleted()
                    .OrderByDescending(s => s.CreatedOn)
                    .ProjectTo<SectionViewModel>();

            return this.View(sections);
        }
    }
}