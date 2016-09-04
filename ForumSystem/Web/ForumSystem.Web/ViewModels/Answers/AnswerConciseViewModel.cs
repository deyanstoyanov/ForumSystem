namespace ForumSystem.Web.ViewModels.Answers
{
    using System;

    using AutoMapper;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;

    public class AnswerConciseViewModel : IMapFrom<Answer>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Answer, AnswerConciseViewModel>()
                .ForMember(a => a.Author, config => config.MapFrom(a => a.Author.UserName));
        }
    }
}