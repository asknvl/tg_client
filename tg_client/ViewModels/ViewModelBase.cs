using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace tg_client.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        public virtual async Task OnLoad()
        {
            await Task.CompletedTask;
        }
    }
}
