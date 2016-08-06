namespace ForumSystem.Web.Areas.Administration.ViewModels.Categories
{
    using System;
    using System.Linq;

    using AutoMapper;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;

    public class CategoryViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int SectionId { get; set; }

        public string Section { get; set; }

        public int ActivePostsCount { get; set; }

        public int DeletedPostsCount { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Category, CategoryViewModel>()
                .ForMember(c => c.Section, config => config.MapFrom(c => c.Section.Title));
            configuration.CreateMap<Category, CategoryViewModel>()
                .ForMember(c => c.ActivePostsCount, config => config.MapFrom(c => c.Posts.Count(p => !p.IsDeleted)));
            configuration.CreateMap<Category, CategoryViewModel>()
                .ForMember(c => c.DeletedPostsCount, config => config.MapFrom(c => c.Posts.Count(p => p.IsDeleted)));
        }
    }
}