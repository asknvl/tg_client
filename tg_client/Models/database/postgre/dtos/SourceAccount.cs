using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tg_client.ViewModels;

namespace tg_client.Models.database.postgre.dtos
{
    public class SourceAccount
    {
        public string source_name { get; set; }
        public  Guid account_id { get; set; }
    }
}
