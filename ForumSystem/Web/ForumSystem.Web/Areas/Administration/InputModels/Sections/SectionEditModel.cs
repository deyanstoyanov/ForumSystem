namespace ForumSystem.Web.Areas.Administration.InputModels.Sections
{
    public class SectionEditModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsDeleted { get; set; }
    }
}