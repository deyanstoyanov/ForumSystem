namespace ForumSystem.Data.Models
{
    public class PostUpdate : Update
    {
        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}