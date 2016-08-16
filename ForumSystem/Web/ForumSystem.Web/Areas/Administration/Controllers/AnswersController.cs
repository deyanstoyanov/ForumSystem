namespace ForumSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Areas.Administration.Controllers.Base;
    using ForumSystem.Web.Areas.Administration.ViewModels.Answers;

    public class AnswersController : AdministrationController
    {
        public AnswersController(IForumSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult All()
        {
            var posts =
                this.Data.Answers.AllWithDeleted()
                    .OrderByDescending(p => p.CreatedOn)
                    .ProjectTo<AnswerViewModel>();

            return this.View(posts);
        }
    }
}