using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tg_client.Models.rest.dtos
{
    public class messageDto()
    {
        public Guid account_id { get; set; }
        public Guid chat_id { get; set; }
        public long user_telegram_id { get; set; }
        public List<mediaDto>? media { get; set; }
        public string text { get; set; }
    }

    public class mediaDto()
    {
        public string type { get; set; }
        public string url { get; set; }
        public string file_id { get; set; }
    }
}
