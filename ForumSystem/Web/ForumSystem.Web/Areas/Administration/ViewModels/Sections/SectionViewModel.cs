namespace ForumSystem.Web.Areas.Administration.ViewModels.Sections
{
    using System;
    using System.Linq;

    using AutoMapper;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;

    public class SectionViewModel : IMapFrom<Section>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int ActiveCategoriesCount { get; set; }

        public int DeletedCategoriesCount { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Section, SectionViewModel>()
                .ForMember(
                    s => s.ActiveCategoriesCount, 
                    config => config.MapFrom(s => s.Categories.Count(c => !c.IsDeleted)));
            configuration.CreateMap<Section, SectionViewModel>()
                .ForMember(
                    s => s.DeletedCategoriesCount, 
                    config => config.MapFrom(s => s.Categories.Count(c => c.IsDeleted)));
        }
    }
}