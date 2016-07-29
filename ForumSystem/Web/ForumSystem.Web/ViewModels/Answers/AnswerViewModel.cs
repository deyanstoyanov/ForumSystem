namespace ForumSystem.Web.ViewModels.Answers
{
    using System;
    using System.Linq;

    using AutoMapper;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;

    public class AnswerViewModel : IMapFrom<Answer>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int PostId { get; set; }

        public string AuthorId { get; set; }

        public string AuthorPictureUrl { get; set; }

        public string Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int CommentsCount { get; set; }

        public bool HasComments { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Answer, AnswerViewModel>()
                .ForMember(a => a.Author, config => config.MapFrom(a => a.Author.UserName));
            configuration.CreateMap<Answer, AnswerViewModel>()
                .ForMember(a => a.AuthorPictureUrl, config => config.MapFrom(a => a.Author.PictureUrl));
            configuration.CreateMap<Answer, AnswerViewModel>()
                .ForMember(a => a.CommentsCount, config => config.MapFrom(a => a.Comments.Count));
            configuration.CreateMap<Answer, AnswerViewModel>()
                .ForMember(a => a.HasComments, config => config.MapFrom(a => a.Comments.Any()));
        }
    }
}