using BM.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.DesktopUI.Events
{
    public class SelectedClientEvent
    {
        public ClientModel SelectedClient { get; set; }

        public SelectedClientEvent(ClientModel client)
        {
            SelectedClient = client;
        }
    }
}
