namespace ForumSystem.Web.ViewModels.PostUpdates
{
    using AutoMapper;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;

    public class PostUpdateViewModel : IMapFrom<PostUpdate>, IHaveCustomMappings
    {
        public int PostId { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }

        public string Reason { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<PostUpdate, PostUpdateViewModel>()
                .ForMember(u => u.Author, config => config.MapFrom(u => u.Author.UserName));
        }
    }
}