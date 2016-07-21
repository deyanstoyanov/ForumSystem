namespace ForumSystem.Web.ViewModels.Comment
{
    using System;

    using AutoMapper;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;
    using ForumSystem.Web.ViewModels.Post;

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings, IPostAnswer
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int AnswerId { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                .ForMember(c => c.Author, config => config.MapFrom(c => c.Author.UserName));
        }
    }
}