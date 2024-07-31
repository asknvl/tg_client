using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tg_client.ViewModels;

namespace tg_engine.database.postgre.models
{
    public class telegram_user : ViewModelBase
    {
        public Guid id { get; set; }
        public long telegram_id { get; set; }
        public string? firstname { get; set; }
        public string? lastname { get; set; }
        public string? username { get; set; }        
    }
}
