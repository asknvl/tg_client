using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tg_engine.config;
using tg_engine.database.mongo;
using tg_engine.database.postgre;

namespace tg_client.ViewModels
{    
    public class mainVM : ViewModelBase
    {
        #region vars
        IPostgreProvider postgreProvider;
        IMongoProvider mongoProvider;
        #endregion

        #region properties
        chatsListVM chatsList;
        public chatsListVM ChatsList
        {
            get => chatsList;
            set => this.RaiseAndSetIfChanged(ref chatsList, value);
        }

        dialogsContainerVM dialogs;
        public dialogsContainerVM Dialogs
        {
            get => dialogs;
            set => this.RaiseAndSetIfChanged(ref dialogs, value);   
        }
        #endregion

        public mainVM()
        {
            var vars = variables.getInstance();
            postgreProvider = new PostgreProvider(vars.tg_engine_variables.accounts_settings_db);
            mongoProvider = new MongoProvider(vars.tg_engine_variables.messaging_settings_db);

            Dialogs = new dialogsContainerVM(mongoProvider);

            ChatsList = new chatsListVM(postgreProvider);
            ChatsList.ChatSelectedEvent += (userchat) => {
                Dialogs.ShowDialog(userchat);
            };

            Task.Run(async () => {

                await ChatsList.OnLoad();

            });

        }

        #region public
        #endregion

        public override async Task OnLoad()
        {
            await ChatsList.OnLoad();
        }
    }
}
