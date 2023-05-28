using Shared_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
    public  class NotificationBase
    {
        public NotificationTopic Topic { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Dictionary<string, string> Data { get; set; }
        public string Sound { get; set; }
        public int Badge { get; set; }
        public int Priority { get; set; }
        public TimeSpan TimeToLive { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public string ClickAction { get; set; }
        public string Tag { get; set; }
    }
}
