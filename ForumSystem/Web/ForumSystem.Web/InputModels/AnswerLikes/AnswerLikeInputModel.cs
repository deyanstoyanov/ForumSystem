namespace ForumSystem.Web.InputModels.AnswerLikes
{
    public class AnswerLikeInputModel
    {
        public int AnswerId { get; set; }

        public string AnswerAuthorId { get; set; }

        public int LikesCount { get; set; }

        public bool IsLiked { get; set; }
    }
}