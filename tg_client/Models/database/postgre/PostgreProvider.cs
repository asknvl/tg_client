using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using tg_engine.config;
using tg_engine.database.postgre.models;
using System.Linq;
using tg_client.Models.database.postgre.dtos;

namespace tg_engine.database.postgre
{
    public class PostgreProvider : IPostgreProvider
    {
        #region vars
        private readonly DbContextOptions<PostgreDbContext> dbContextOptions;
        object lockObj = new object();
        #endregion

        public PostgreProvider(settings_db settings)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PostgreDbContext>();
            optionsBuilder.UseNpgsql($"Host={settings.host};Username={settings.user};Password={settings.password};Database={settings.db_name};Pooling=true;");
            dbContextOptions = optionsBuilder.Options;
        }

        public async Task<List<account>> GetAccountsAsync()
        {
            using (var context = new PostgreDbContext(dbContextOptions))
            {
                return await context.accounts.ToListAsync();
            }
        }

        public async Task<List<channel_account>> GetChannelsAccounts()
        {
            using (var context = new PostgreDbContext(dbContextOptions))
            {
                return await context.channels_accounts.ToListAsync();
            }
        }

        public async Task<List<SourceAccount>> GetSourcesAccounts()
        {
            using (var context = new PostgreDbContext(dbContextOptions))
            {
                var query = from account in context.accounts
                            join channelAccount in context.channels_accounts on account.id equals channelAccount.account_id
                            join channel in context.channels on channelAccount.channel_id equals channel.id
                            join source in context.sources on channel.id equals source.channel_id
                            select new
                            {
                                source = source.source_name,
                                account_id = account.id
                            };

                List<SourceAccount> res = new();

                foreach (var q in query)
                {
                    res.Add(new SourceAccount()
                    {

                        source_name = q.source,
                        account_id = q.account_id

                    });
                }

                return res;
            }
        }

        public async Task<List<UserChat>> GetUserChats(Guid account_id)
        {
            List<UserChat> res = new();

            using (var context = new PostgreDbContext(dbContextOptions))
            {

                var foundChat = (from chat in context.telegram_chats
                                join user in context.telegram_users
                                on chat.telegram_user_id equals user.id
                                where chat.account_id == account_id
                                orderby chat.updated_at descending
                                select new
                                {
                                    telegram_chat = chat,
                                    telegram_user = user
                                }).Take(100);

                foreach (var chat in foundChat)
                {
                    res.Add(new UserChat { 
                        chat = chat.telegram_chat,
                        user = chat.telegram_user,
                    });                    
                }                              
            }

            return res.OrderByDescending(c => c.chat.updated_at).ToList();
        }
    }
}

