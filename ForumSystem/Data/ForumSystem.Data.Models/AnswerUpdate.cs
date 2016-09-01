namespace ForumSystem.Data.Models
{
    public class AnswerUpdate : Update
    {
        public int AnswerId { get; set; }

        public virtual Answer Answer { get; set; }
    }
}