namespace ForumSystem.Web.ViewModels.Category
{
    using System.Collections.Generic;

    using AutoMapper;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;
    using ForumSystem.Web.ViewModels.Post;

    public class CategoryViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Section { get; set; }

        public IEnumerable<PostConciseViewModel> Posts { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Category, CategoryViewModel>()
                .ForMember(c => c.Section, config => config.MapFrom(c => c.Section.Title));
            configuration.CreateMap<Category, CategoryViewModel>()
                .ForMember(c => c.Posts, config => config.MapFrom(c => c.Posts));
        }
    }
}