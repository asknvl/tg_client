using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tg_client.Models.rest.updates
{
    public class readHistory : UpdateBase
    {
        public int max_id { get; set; }
    }
}
