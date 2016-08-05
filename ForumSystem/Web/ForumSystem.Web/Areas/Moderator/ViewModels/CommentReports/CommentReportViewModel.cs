namespace ForumSystem.Web.Areas.Moderator.ViewModels.CommentReports
{
    using System;

    using AutoMapper;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;

    public class CommentReportViewModel : IMapFrom<CommentReport>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }

        public int PostId { get; set; }

        public string Post { get; set; }

        public int AnswerId { get; set; }

        public int CommentId { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<CommentReport, CommentReportViewModel>()
                .ForMember(c => c.Author, config => config.MapFrom(c => c.Author.UserName));
            configuration.CreateMap<CommentReport, CommentReportViewModel>()
                .ForMember(c => c.Post, config => config.MapFrom(c => c.Comment.Answer.Post.Title));
            configuration.CreateMap<CommentReport, CommentReportViewModel>()
                .ForMember(c => c.AnswerId, config => config.MapFrom(c => c.Comment.AnswerId));
        }
    }
}