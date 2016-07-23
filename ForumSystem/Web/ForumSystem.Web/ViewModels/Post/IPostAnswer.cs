namespace ForumSystem.Web.ViewModels.Post
{
    using System;

    public interface IPostAnswer
    {
        int Id { get; }

        string Content { get; }

        string AuthorId { get; }

        string AuthorPictureUrl { get; }

        string Author { get; }

        DateTime CreatedOn { get; }
    }
}