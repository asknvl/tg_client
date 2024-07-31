using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tg_client.Models.database.postgre.dtos;
using tg_engine.database.postgre;
using tg_engine.database.postgre.models;
using tg_engine.tg_hub.events;

namespace tg_client.ViewModels
{
    public class chatsListVM : ViewModelBase
    {
        #region vars        
        IPostgreProvider postgreProvider;
        #endregion

        #region properties        
        List<SourceAccount> sources;
        public List<SourceAccount> Sources
        {
            get => sources;
            set => this.RaiseAndSetIfChanged(ref sources, value);   
        }

        SourceAccount source;
        public SourceAccount Source
        {
            get => source;
            set
            {
                this.RaiseAndSetIfChanged(ref source, value);
                updateChats(source);
            }
        }

        UserChat chat;
        public UserChat Chat
        {
            get => chat;
            set
            {
                this.RaiseAndSetIfChanged(ref chat, value);
                if (value != null)
                    ChatSelectedEvent?.Invoke(chat);
            }
        }


        public ObservableCollection<UserChat> Chats { get; } = new();
        #endregion

        public chatsListVM(IPostgreProvider postgreProvider)
        {           
            this.postgreProvider = postgreProvider;
        }

        #region helpers
        async Task updateChats(SourceAccount source)
        {
            Chats.Clear();

            var chats = await postgreProvider.GetUserChats(source.account_id);
            foreach (var chat in chats)
            {
                Chats.Add(chat);
            }
        }
        #endregion

        #region public
        public override async Task OnLoad()
        {

            Sources = await postgreProvider.GetSourcesAccounts();
            if (Sources.Count > 0)
                Source = Sources[0];

            //var res = await postgreProvider.GetUserChats();
        }

        public async Task OnNewChat(newChatEvent echt)
        {

            if (Source.account_id != echt.account_id)
                return;

            var found = Chats.SingleOrDefault(c => c.chat.id == echt.chat_id);
            if (found == null)
            {
                Chat = null;

                Chats.Insert(0, new UserChat()
                {
                    chat = new telegram_chat()
                    {
                        id = echt.chat_id,
                        account_id = echt.account_id,
                        unread_count = 1
                    },
                    user = new telegram_user()
                    {
                        telegram_id = echt.telegram_id,
                        firstname = echt.data.firstname,
                        lastname = echt.data.lastname,
                        username = echt.data.username
                    }
                });
            }
        }

        public async Task OnNewMessage(newMessageEvent emsg)
        {
            var found = Chats.SingleOrDefault(c => c.chat.id == emsg.chat_id);
            if (found != null)
            {
                var tmpChat = Chat;
                Chat = null;

                found.chat.unread_count = found.chat.unread_count++ ?? 1;
                Chats.Remove(found);                   
                Chats.Insert(0, found);

                //Chat = tmpChat;



            }
        }
        #endregion

        #region events
        public event Action<UserChat> ChatSelectedEvent;
        #endregion
    }
}
