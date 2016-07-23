namespace ForumSystem.Web.ViewModels
{
    using System.Collections.Generic;

    using ForumSystem.Web.ViewModels.Category;
    using ForumSystem.Web.ViewModels.Section;

    public class SidebarViewModel
    {
        public IEnumerable<CategoryConciseViewModel> Categories { get; set; }

        public IEnumerable<SectionViewModel> Sections { get; set; }
    }
}