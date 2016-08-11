namespace ForumSystem.Web.ViewModels.LastActivities
{
    using System;

    public class PostLastActivityViewModel
    {
        public int AnswerId { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}