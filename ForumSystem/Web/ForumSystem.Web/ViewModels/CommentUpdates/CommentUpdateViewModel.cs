namespace ForumSystem.Web.ViewModels.CommentUpdates
{
    using AutoMapper;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;

    public class CommentUpdateViewModel : IMapFrom<CommentUpdate>, IHaveCustomMappings
    {
        public int CommentId { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }

        public string Reason { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<CommentUpdate, CommentUpdateViewModel>()
                .ForMember(u => u.Author, config => config.MapFrom(u => u.Author.UserName));
        }
    }
}