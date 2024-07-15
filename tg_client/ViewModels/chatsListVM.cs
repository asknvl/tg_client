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
        #endregion

        #region events
        public event Action<UserChat> ChatSelectedEvent;
        #endregion
    }
}
