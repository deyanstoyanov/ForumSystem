namespace ForumSystem.Web.ViewModels
{
    using System.Collections.Generic;

    using ForumSystem.Web.ViewModels.Categories;
    using ForumSystem.Web.ViewModels.Sections;

    public class SidebarViewModel
    {
        public IEnumerable<CategoryConciseViewModel> Categories { get; set; }

        public IEnumerable<SectionViewModel> Sections { get; set; }
    }
}