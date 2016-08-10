namespace ForumSystem.Data.Models
{
    public class AnswerLike : Like
    {
        public int AnswerId { get; set; }

        public virtual Answer Answer { get; set; }
    }
}