namespace ForumSystem.Web.ViewModels.AnswerUpdates
{
    using AutoMapper;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;

    public class AnswerUpdateViewModel : IMapFrom<AnswerUpdate>, IHaveCustomMappings
    {
        public int AnswerId { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }

        public string Reason { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<AnswerUpdate, AnswerUpdateViewModel>()
                .ForMember(u => u.Author, config => config.MapFrom(u => u.Author.UserName));
        }
    }
}