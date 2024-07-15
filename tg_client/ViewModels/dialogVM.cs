using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using tg_client.Models.database.postgre.dtos;
using tg_client.Models.rest;
using tg_engine.database.mongo;
using tg_engine.database.postgre;
using Tmds.DBus.Protocol;

namespace tg_client.ViewModels
{
    public class dialogVM : ViewModelBase
    {
        #region vars
        IMongoProvider mongoProvider;
        ITGEngineApi api;
        UserChat userChat;
        #endregion

        #region properties
        ObservableCollection<messageVM> Messages { get; } = new();

        string text;
        public string Text
        {
            get => text;
            set => this.RaiseAndSetIfChanged(ref text, value);  
        }
        #endregion

        #region commands
        public ReactiveCommand<Unit, Unit> sendMessageCmd { get; }
        #endregion

        public dialogVM(IMongoProvider mongoProvider, ITGEngineApi api, UserChat userChat)
        {
            this.mongoProvider = mongoProvider;
            this.api = api;
            this.userChat = userChat;

            #region commands
            sendMessageCmd = ReactiveCommand.CreateFromTask(async () => {

            });
            #endregion
        }

        #region public
        public async Task update()
        {

            long? lastID = 0;
            if (Messages.Count > 0)
                lastID = Messages.Last().message_id;

            var messages = await mongoProvider.GetMessages(userChat.chat.id, lastID);

            for (var i = messages.Count-1; i >= 0; i--)
            {
                var message = messages[i];
                var msg = new messageVM(message.telegram_message_id, message.direction, message.text, message.date);

                Messages.Add(msg);
            }

            //foreach (var message in messages)
            //{
            //    //Messages.Add(new messageVM(message.telegram_message_id, message.direction, message.text, message.date));

            //    int index = (Messages.Count > 0) ? Messages.Count-1 : 0;
            //    var msg = new messageVM(message.telegram_message_id, message.direction, message.text, message.date);
            //    Messages.Insert(index, msg);
            //}
        }
        #endregion
    }
}
