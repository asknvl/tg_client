
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tg_client.Models.database.postgre.dtos;
using tg_client.Models.rest;
using tg_engine.database.mongo;

namespace tg_client.ViewModels
{
    public class dialogsContainerVM : ViewModelBase
    {
        #region vars
        IMongoProvider mongoProvider;
        ITGEngineApi api;
        #endregion

        #region properties
        object content;
        public object Content {
            get => content;
            set => this.RaiseAndSetIfChanged(ref content, value);
        }
        Dictionary<Guid, dialogVM> dialogs { get; } = new();
        #endregion

        public dialogsContainerVM(IMongoProvider mongoProvider, ITGEngineApi api)
        {
            this.mongoProvider = mongoProvider;
            this.api = api;
           
        }

        #region public
        public async Task ShowDialog(UserChat userChat)
        {

            if (userChat == null)
                return;

            dialogVM dialog;
            var chat_id = userChat.chat.id;

            if (dialogs.ContainsKey(chat_id))
                dialog = dialogs[chat_id];
            else
            {
                dialog = new dialogVM(mongoProvider, api, userChat);
                dialogs.Add(chat_id, dialog);
            }

            Content = dialog;
            await dialog.update();
        }
        #endregion
    }
}
