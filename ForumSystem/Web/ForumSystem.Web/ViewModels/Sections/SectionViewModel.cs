namespace ForumSystem.Web.ViewModels.Sections
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;
    using ForumSystem.Web.ViewModels.Categories;

    public class SectionViewModel : IMapFrom<Section>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<CategoryConciseViewModel> Categories { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Section, SectionViewModel>()
                .ForMember(s => s.Categories, config => config.MapFrom(s => s.Categories.Where(c => !c.IsDeleted)));
        }
    }
}