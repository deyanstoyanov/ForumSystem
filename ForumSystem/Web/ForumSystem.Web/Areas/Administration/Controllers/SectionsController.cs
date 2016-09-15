namespace ForumSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.Models;
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Areas.Administration.Controllers.Base;
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
                    .ProjectTo<SectionViewModel>()
                    .ToList();

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

        [HttpGet]
        public ActionResult Edit(int? id)
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

            var model = new SectionEditModel { Id = section.Id, Title = section.Title, IsDeleted = section.IsDeleted };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SectionEditModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var section = this.Data.Sections.GetById(model.Id);

                section.Title = model.Title;
                section.IsDeleted = model.IsDeleted;

                this.Data.Sections.Update(section);
                this.Data.SaveChanges();

                return this.RedirectToAction("All");
            }

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
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

            var model = Mapper.Map<SectionConciseViewModel>(section);

            return this.PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var section = this.Data.Sections.GetById(id);
            if (section == null)
            {
                return this.HttpNotFound();
            }

            this.Data.Sections.Delete(id);
            this.Data.SaveChanges();

            return this.RedirectToAction("All");
        }
    }
}