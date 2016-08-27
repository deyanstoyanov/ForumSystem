namespace ForumSystem.Web.ViewModels.LastActivities
{
    using System;

    public class CategoryLastPostViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string AuthorId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}