namespace ForumSystem.Web.ViewModels.LastActivities
{
    using ForumSystem.Web.ViewModels.Answers;
    using ForumSystem.Web.ViewModels.Comments;

    public class PostLastActivityViewModel
    {
        public AnswerConciseViewModel Answer { get; set; }

        public CommentConciseViewModel Comment { get; set; }
    }
}