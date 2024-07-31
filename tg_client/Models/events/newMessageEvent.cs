using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tg_engine.interlayer.messaging;

namespace tg_engine.tg_hub.events
{
    public class newMessageEvent : EventBase
    {
        public messageData data { get; set; }
    }

    public class messageData()
    {
        public Guid chat_id { get; set; }
        public string direction { get; set; }
        public long telegram_id { get; set; }
        public int telegram_message_id { get; set; }
        public string? text { get; set; }
        public DateTime date { get; set; }
        public int? reply_to_message_id { get; set; }
        public List<MediaInfo>? media { get; set; }
        public bool is_business_bot_reply { get; set; }
        public string? business_bot_username { get; set; }
    }
}
