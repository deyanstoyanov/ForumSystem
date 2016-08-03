namespace ForumSystem.Web.Areas.Moderator.ViewModels.AnswerReports
{
    using System;

    using AutoMapper;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;

    public class AnswerReportViewModel : IMapFrom<AnswerReport>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }

        public int AnswerId { get; set; }

        public int PostId { get; set; }

        public string Post { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<AnswerReport, AnswerReportViewModel>()
                .ForMember(a => a.Author, config => config.MapFrom(a => a.Author.UserName));
            configuration.CreateMap<AnswerReport, AnswerReportViewModel>()
                .ForMember(a => a.Post, config => config.MapFrom(a => a.Answer.Post.Title));
            configuration.CreateMap<AnswerReport, AnswerReportViewModel>()
                .ForMember(a => a.PostId, config => config.MapFrom(a => a.Answer.PostId));
        }
    }
}