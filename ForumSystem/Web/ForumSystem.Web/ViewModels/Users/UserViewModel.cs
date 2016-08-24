namespace ForumSystem.Web.ViewModels.Users
{
    using System;
    using System.Linq;

    using AutoMapper;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;

    public class UserViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public string PictureUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public int PostsCount { get; set; }

        public int AnswersCount { get; set; }

        public int CommentsCount { get; set; }

        public string WebsiteUrl { get; set; }

        public string Occupation { get; set; }

        public string Interests { get; set; }

        public string AboutMe { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string GitHubProfile { get; set; }

        public string StackOverflowProfile { get; set; }

        public string LinkedInProfile { get; set; }

        public string FacebookProfile { get; set; }

        public string TwitterProfile { get; set; }

        public string SkypeProfile { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(u => u.PostsCount, config => config.MapFrom(u => u.Posts.Count(p => !p.IsDeleted)));
            configuration.CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(u => u.AnswersCount, config => config.MapFrom(u => u.Answers.Count(a => !a.IsDeleted)));
            configuration.CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(u => u.CommentsCount, config => config.MapFrom(u => u.Comments.Count(c => !c.IsDeleted)));
        }
    }
}