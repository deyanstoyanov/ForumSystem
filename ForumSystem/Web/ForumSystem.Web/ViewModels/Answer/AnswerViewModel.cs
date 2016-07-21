namespace ForumSystem.Web.ViewModels.Answer
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;
    using ForumSystem.Web.ViewModels.Comment;
    using ForumSystem.Web.ViewModels.Post;

    public class AnswerViewModel : IMapFrom<Answer>, IHaveCustomMappings, IPostAnswer
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int PostId { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Answer, AnswerViewModel>()
                .ForMember(a => a.Author, config => config.MapFrom(a => a.Author.UserName));
            configuration.CreateMap<Answer, AnswerViewModel>()
                .ForMember(a => a.Comments, config => config.MapFrom(a => a.Comments));
        }
    }
}