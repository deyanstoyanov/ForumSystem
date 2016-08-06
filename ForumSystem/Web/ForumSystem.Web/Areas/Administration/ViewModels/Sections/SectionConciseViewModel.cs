namespace ForumSystem.Web.Areas.Administration.ViewModels.Sections
{
    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;

    public class SectionConciseViewModel : IMapFrom<Section>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}