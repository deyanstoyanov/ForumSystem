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
    using ForumSystem.Web.Areas.Administration.InputModels.Categories;
    using ForumSystem.Web.Areas.Administration.ViewModels.Categories;
    using ForumSystem.Web.Areas.Administration.ViewModels.Sections;

    public class CategoriesController : AdministrationController
    {
        public CategoriesController(IForumSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult All()
        {
            var categories =
                this.Data.Categories.AllWithDeleted()
                    .OrderByDescending(c => c.CreatedOn)
                    .ProjectTo<CategoryViewModel>()
                    .ToList();

            return this.View(categories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var sections = this.Data.Sections.All().ProjectTo<SectionConciseViewModel>().ToList();
            var inputModel = new CategoryInputModel { Sections = new SelectList(sections, "Id", "Title") };

            return this.View(inputModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryInputModel input)
        {
            if (input != null && this.ModelState.IsValid)
            {
                var category = new Category
                                   {
                                       SectionId = input.SectionId, 
                                       Title = input.Title, 
                                       Description = input.Description
                                   };

                this.Data.Categories.Add(category);
                this.Data.SaveChanges();

                return this.RedirectToAction("All");
            }

            return this.View(input);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
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

            var model = Mapper.Map<CategoryViewModel>(category);

            return this.PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var report = this.Data.Categories.GetById(id);
            if (report == null)
            {
                return this.HttpNotFound();
            }

            this.Data.Categories.Delete(id);
            this.Data.SaveChanges();

            return this.RedirectToAction("All");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
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

            var sections = this.Data.Sections.All().ProjectTo<SectionConciseViewModel>().ToList();
            var model = new CategoryEditModel
                            {
                                Id = category.Id, 
                                Title = category.Title, 
                                Description = category.Description, 
                                SectionId = category.SectionId,
                                IsDeleted = category.IsDeleted,
                                Sections = new SelectList(sections, "Id", "Title", category.SectionId)
                            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryEditModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var category = this.Data.Categories.GetById(model.Id);

                category.Title = model.Title;
                category.Description = model.Description;
                category.SectionId = model.SectionId;
                category.IsDeleted = model.IsDeleted;

                this.Data.Categories.Update(category);
                this.Data.SaveChanges();

                return this.RedirectToAction("All");
            }

            return this.View(model);
        }
    }
}