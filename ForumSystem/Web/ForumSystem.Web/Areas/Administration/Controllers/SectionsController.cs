namespace ForumSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.Models;
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Areas.Administration.InputModels.Sections;
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

        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SectionInputModel input)
        {
            if (input != null && this.ModelState.IsValid)
            {
                var section = new Section { Title = input.Title };

                this.Data.Sections.Add(section);
                this.Data.SaveChanges();

                return this.RedirectToAction("All");
            }

            return this.View(input);
        }
    }
}