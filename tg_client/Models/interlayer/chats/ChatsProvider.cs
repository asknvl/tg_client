using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tg_client.Models.database.postgre.dtos;
using tg_engine.database.postgre;
using tg_engine.database.postgre.models;

namespace tg_engine.interlayer.chats
{
    public class ChatsProvider : IChatsProvider
    {
        #region vars
        IPostgreProvider postgreProvider;
        List<UserChat> userChats = new();
        #endregion

        public ChatsProvider(IPostgreProvider postgreProvider) { 
            this.postgreProvider = postgreProvider;
        }       
    }
}
