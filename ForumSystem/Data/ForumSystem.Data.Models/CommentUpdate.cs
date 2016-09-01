namespace ForumSystem.Data.Models
{
    public class CommentUpdate : Update
    {
        public int CommentId { get; set; }

        public virtual Comment Comment { get; set; }
    }
}