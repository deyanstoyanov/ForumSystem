namespace ForumSystem.Web.ViewModels.Notifications
{
    using System;

    using AutoMapper;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;

    public class NotificationViewModel : IMapFrom<Notification>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int? ItemId { get; set; }

        public string SenderId { get; set; }

        public string Sender { get; set; }

        public string ReceiverId { get; set; }

        public string Receiver { get; set; }

        public NotificationType NotificationType { get; set; }

        public bool IsChecked { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Notification, NotificationViewModel>()
                .ForMember(n => n.Receiver, config => config.MapFrom(n => n.Receiver.UserName));
            configuration.CreateMap<Notification, NotificationViewModel>()
                .ForMember(n => n.Sender, config => config.MapFrom(n => n.Sender.UserName));
        }
    }
}