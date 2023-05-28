using Base.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repostry.NotificationService;

namespace HundedPlus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Notification : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public Notification(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<string> GetNotification(string title, string body)
        {
            var notification = new NotificationBase
            {
                Title = title,
                Body = body,
                Topic = Shared_Core.NotificationTopic.Meals
            };
            var x=_notificationService.SendNotification(title, body);
            return x;


        }
}
}
