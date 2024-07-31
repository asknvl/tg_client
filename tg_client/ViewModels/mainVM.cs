using ReactiveUI;
using System.Threading.Tasks;
using tg_client.Models.rest;
using tg_engine.config;
using tg_engine.database.mongo;
using tg_engine.database.postgre;
using SocketIOClient;
using Newtonsoft.Json;
using tg_engine.tg_hub.events;
using System;
using System.Collections.Generic;

namespace tg_client.ViewModels
{    
    public class mainVM : ViewModelBase
    {
        #region vars
        IPostgreProvider postgreProvider;
        IMongoProvider mongoProvider;
        ITGEngineApi api;
        SocketIOClient.SocketIO socket;
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
            api = new TGEngineApi("http://localhost:8080");

            Dialogs = new dialogsContainerVM(mongoProvider, api);

            ChatsList = new chatsListVM(postgreProvider);
            ChatsList.ChatSelectedEvent += async (userchat) => {
                await Dialogs.ShowDialog(userchat);
            };

            socket = new SocketIOClient.SocketIO("http://192.168.119.53:3000");
            socket.On("new-chat", async response => {
               try
                {
                    var eChts = JsonConvert.DeserializeObject<List<newChatEvent>>(response.ToString());
                    foreach (var e in eChts)
                        await ChatsList.OnNewChat(e);
                } catch (Exception ex)
                {

                }
            });
            socket.On("new-message", async response => {
                try
                {
                    var eMsgs = JsonConvert.DeserializeObject<List<newMessageEvent>>(response.ToString());
                    foreach (var e in eMsgs)
                        await ChatsList.OnNewMessage(e);
                   
                } catch (Exception ex)
                {

                }                
            });


            Task.Run(async () =>
            {                
                await ChatsList.OnLoad();
                await socket.ConnectAsync();
            });

        }

        #region public
        #endregion

        public override async Task OnLoad()
        {
          
        }
    }
}
