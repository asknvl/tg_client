using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tg_engine.database.postgre.models;

namespace tg_engine.tg_hub.events
{
    public class newChatEvent : EventBase
    {
        public chatData data { get; set; } 
    }

    public class chatData
    {
        public string? firstname { get; set; }
        public string? lastname { get; set; }
        public string? username { get; set; }
    }
}
