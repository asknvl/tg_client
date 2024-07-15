using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tg_client.Models.rest.dtos;

namespace tg_client.Models.rest
{
    public interface ITGEngineApi
    {
        Task SendMessage(messageDto message);
    }
}
