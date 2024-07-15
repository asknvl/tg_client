using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tg_client.Models.database.postgre.dtos;
using tg_engine.database.postgre.models;

namespace tg_engine.database.postgre
{
    public interface IPostgreProvider
    {
        Task<List<account>> GetAccountsAsync();
        Task<List<channel_account>> GetChannelsAccounts();
        Task<List<SourceAccount>> GetSourcesAccounts();
        Task<List<UserChat>> GetUserChats(Guid account_id);
    }
}
