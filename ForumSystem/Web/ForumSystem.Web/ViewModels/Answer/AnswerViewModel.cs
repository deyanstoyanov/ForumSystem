namespace ForumSystem.Web.ViewModels.Answer
{
    using System;

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

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Answer, AnswerViewModel>()
                .ForMember(a => a.Author, config => config.MapFrom(a => a.Author.UserName));
            configuration.CreateMap<Answer, AnswerViewModel>()
                .ForMember(a => a.AuthorPictureUrl, config => config.MapFrom(a => a.Author.PictureUrl));
        }
    }
}