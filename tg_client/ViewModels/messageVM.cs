using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tg_client.ViewModels
{
    public class messageVM : ViewModelBase
    {
        #region properties
        public long? message_id { get; }        

        string direction;
        public string Direction
        {
            get => direction;
            set => this.RaiseAndSetIfChanged(ref direction, value); 
        }

        string text;
        public string Text
        {
            get => text;
            set => this.RaiseAndSetIfChanged(ref text, value);  
        }

        DateTime time;
        public DateTime Time
        {
            get => time;
            set => this.RaiseAndSetIfChanged(ref time, value);  
        }
        #endregion

        public messageVM(long? message_id, string direction, string text, DateTime time)
        {
            this.message_id = message_id;            
            Direction = direction;            
            Text = text;            
            Time = time;
        }
    }
}
