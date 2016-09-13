namespace ForumSystem.Web.ViewModels.Posts
{
    using System;
    using System.Linq;

    using AutoMapper;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;

    public class PostConciseViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorPictureUrl { get; set; }

        public string Author { get; set; }

        public string AuthorId { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CategoryId { get; set; }

        public string Category { get; set; }

        public int Views { get; set; }

        public int AnswersCount { get; set; }

        public int ReportsCount { get; set; }

        public int LikesCount { get; set; }

        public bool IsLocked { get; set; }

        public bool IsPinned { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Post, PostConciseViewModel>()
                .ForMember(p => p.Author, config => config.MapFrom(p => p.Author.UserName));
            configuration.CreateMap<Post, PostConciseViewModel>()
                .ForMember(p => p.Category, config => config.MapFrom(p => p.Category.Title));
            configuration.CreateMap<Post, PostConciseViewModel>()
                .ForMember(p => p.AnswersCount, config => config.MapFrom(p => p.Answers.Count(a => !a.IsDeleted)));
            configuration.CreateMap<Post, PostConciseViewModel>()
                .ForMember(p => p.AuthorPictureUrl, config => config.MapFrom(p => p.Author.PictureUrl));
            configuration.CreateMap<Post, PostConciseViewModel>()
                .ForMember(p => p.ReportsCount, config => config.MapFrom(p => p.Reports.Count(r => !r.IsDeleted)));
            configuration.CreateMap<Post, PostConciseViewModel>()
                .ForMember(p => p.LikesCount, config => config.MapFrom(p => p.Likes.Count(l => !l.IsDeleted)));
        }
    }
}