namespace ForumSystem.Data.Models
{
    public class CommentReport : Report
    {
        public int CommentId { get; set; }

        public virtual Comment Comment { get; set; }
    }
}