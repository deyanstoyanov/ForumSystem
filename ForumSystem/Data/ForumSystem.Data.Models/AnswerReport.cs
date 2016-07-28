namespace ForumSystem.Data.Models
{
    public class AnswerReport : Report
    {
        public int AnswerId { get; set; }

        public virtual Answer Answer { get; set; }
    }
}