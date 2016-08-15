namespace ForumSystem.Web.ViewModels.Categories
{
    using System.Linq;

    using AutoMapper;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;

    public class CategoryConciseViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int PostsCount { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Category, CategoryConciseViewModel>()
                .ForMember(c => c.PostsCount, config => config.MapFrom(c => c.Posts.Count(p => !p.IsDeleted)));
        }
    }
}