namespace ForumSystem.Data.Models
{
    public class PostReport : Report
    {
        public int PostId { get; set; }

        public Post Post { get; set; }
    }
}