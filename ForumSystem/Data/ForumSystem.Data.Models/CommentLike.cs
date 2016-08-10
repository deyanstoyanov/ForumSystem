namespace ForumSystem.Data.Models
{
    public class CommentLike : Like
    {
        public int CommentId { get; set; }

        public virtual Comment Comment { get; set; }
    }
}