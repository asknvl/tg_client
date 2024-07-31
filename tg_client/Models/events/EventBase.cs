using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tg_engine.tg_hub.events
{
    public abstract class EventBase
    {   
        public Guid account_id { get; set; }
        public Guid chat_id { get; set; }
        public long telegram_id { get; set; }              
    }   
}
