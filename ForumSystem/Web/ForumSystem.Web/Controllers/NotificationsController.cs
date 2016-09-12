namespace ForumSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Controllers.Base;
    using ForumSystem.Web.ViewModels.Notifications;

    using Microsoft.AspNet.Identity;

    using PagedList;

    using WebGrease.Css.Extensions;

    [Authorize]
    public class NotificationsController : BaseController
    {
        private const int NotificationsOnFirstLoadDefaultValue = 6;

        private const int NotificationsOnAjaxLoadDefaultValue = 4;

        private const int NotificationsPerPageDefaultValue = 10;

        public NotificationsController(IForumSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult All(int? page)
        {
            var userId = this.User.Identity.GetUserId();
            var pageNumber = page ?? 1;
            var notifications =
                this.Data.Notifications.All()
                    .Where(n => n.ReceiverId == userId)
                    .OrderByDescending(n => n.CreatedOn)
                    .ProjectTo<NotificationViewModel>()
                    .ToList();

            this.Data.Notifications.All()
                .Where(n => n.ReceiverId == userId && !n.IsChecked)
                .ForEach(n => n.IsChecked = true);

            this.Data.SaveChanges();

            var model = notifications.ToPagedList(pageNumber, NotificationsPerPageDefaultValue);

            return this.View(model);
        }

        [HttpGet]
        public ActionResult GetLastNotifications()
        {
            var userId = this.User.Identity.GetUserId();
            var lastNotications =
                this.Data.Notifications.All()
                    .Where(n => n.ReceiverId == userId)
                    .OrderBy(n => n.IsChecked)
                    .ThenByDescending(n => n.CreatedOn)
                    .Take(NotificationsOnFirstLoadDefaultValue)
                    .ProjectTo<NotificationViewModel>()
                    .ToList();

            this.Data.Notifications.All()
                .Where(n => n.ReceiverId == userId)
                .OrderBy(n => n.IsChecked)
                .ThenByDescending(n => n.CreatedOn)
                .Take(NotificationsOnFirstLoadDefaultValue)
                .ForEach(n => n.IsChecked = true);

            this.Data.SaveChanges();

            return this.PartialView("_LastNotificationsPartial", lastNotications);
        }

        public ActionResult LoadMoreNotifications(string dateOfLastNotification)
        {
            if (dateOfLastNotification == null)
            {
                return null;
            }

            var date = DateTime.Parse(dateOfLastNotification);
            var userId = this.User.Identity.GetUserId();
            var user = this.Data.Users.GetById(userId);

            var olderNotifications =
                this.Data.Notifications.All()
                    .Where(n => n.ReceiverId == userId && n.CreatedOn < date)
                    .OrderBy(n => n.IsChecked)
                    .ThenByDescending(n => n.CreatedOn)
                    .Take(NotificationsOnAjaxLoadDefaultValue)
                    .ProjectTo<NotificationViewModel>()
                    .ToList();

            this.Data.Notifications.All()
                .Where(n => n.ReceiverId == userId && n.CreatedOn < date)
                .OrderBy(n => n.IsChecked)
                .ThenByDescending(n => n.CreatedOn)
                .Take(NotificationsOnAjaxLoadDefaultValue)
                .ForEach(n => n.IsChecked = true);

            this.Data.SaveChanges();

            this.UpdateNotificationsCount(user);

            return this.PartialView("_LastNotificationsPartial", olderNotifications);
        }

        [HttpGet]
        public ActionResult GetNotificationsCount()
        {
            var userId = this.User.Identity.GetUserId();
            var user = this.Data.Users.All().FirstOrDefault(u => u.Id == userId);
            var notificationsCount = user.ReceivedNotifications.Count(n => !n.IsChecked);

            return this.Content(notificationsCount.ToString());
        }
    }
}