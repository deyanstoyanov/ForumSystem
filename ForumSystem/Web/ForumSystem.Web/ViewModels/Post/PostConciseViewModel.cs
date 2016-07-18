namespace ForumSystem.Web.ViewModels.Post
{
    using System;

    using AutoMapper;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;

    public class PostConciseViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Category { get; set; }

        public int AnswersCount { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Post, PostConciseViewModel>()
                .ForMember(p => p.Author, config => config.MapFrom(p => p.Author.UserName));
            configuration.CreateMap<Post, PostConciseViewModel>()
                .ForMember(p => p.Category, config => config.MapFrom(p => p.Category.Title));
            configuration.CreateMap<Post, PostConciseViewModel>()
                .ForMember(p => p.AnswersCount, config => config.MapFrom(p => p.Answers.Count));
        }
    }
}