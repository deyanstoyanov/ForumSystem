namespace ForumSystem.Web.InputModels.CommentLikes
{
    public class CommentLikeInputModel
    {
        public int CommentId { get; set; }

        public string CommentAuthorId { get; set; }

        public int LikesCount { get; set; }

        public bool IsLiked { get; set; }
    }
}