namespace ForumSystem.Web.Areas.Moderator.ViewModels.PostReports
{
    using System;

    using AutoMapper;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;

    public class PostReportViewModel : IMapFrom<PostReport>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }

        public int PostId { get; set; }

        public string Post { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<PostReport, PostReportViewModel>()
                .ForMember(p => p.Author, config => config.MapFrom(p => p.Author.UserName));
            configuration.CreateMap<PostReport, PostReportViewModel>()
                .ForMember(p => p.Post, config => config.MapFrom(p => p.Post.Title));
        }
    }
}