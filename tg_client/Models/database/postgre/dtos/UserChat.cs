using System;
using tg_client.ViewModels;
using tg_engine.database.postgre.models;

namespace tg_client.Models.database.postgre.dtos
{
    public class UserChat : ViewModelBase
    {

        //public Guid account_id { get; set; }
        //public Guid chat_id { get; set; }
        //public long telegram_id { get; set; }        
        //public string? firstname { get; set; }
        //public string? lastname { get; set; }
        //public string? username { get; set; }

        public telegram_chat chat { get; set; } = new();
        public telegram_user user { get; set; } = new();
    }
}