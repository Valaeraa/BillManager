using BM.DesktopUI.Events;
using BM.DesktopUI.Views;
using BM.Library.DataAccess;
using BM.Library.Models;
using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BM.DesktopUI.ViewModels
{
    public class ClientViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IClientData _data;
        private readonly NewClientEntryViewModel _newClientEntryVM;
        private readonly EditClientViewModel _editClientVM;

        public ClientViewModel(IEventAggregator eventAggregator, IClientData data,
            NewClientEntryViewModel newClientEntryVM, EditClientViewModel editClientVM)
        {
            DisplayName = "Clients";

            _eventAggregator = eventAggregator;
            _data = data;
            _newClientEntryVM = newClientEntryVM;
            _editClientVM = editClientVM;

            GetClients();
        }

        private BindableCollection<ClientModel> _clients = new BindableCollection<ClientModel>();

        public BindableCollection<ClientModel> Clients
        {
            get { return _clients; }
            set { SetAndNotify(ref _clients, value); }
        }

        private ClientModel _selectedClient;

        public ClientModel SelectedClient
        {
            get { return _selectedClient; }
            set 
            {
                SetAndNotify(ref _selectedClient, value);
                NotifyOfPropertyChange(() => CanEditClient);
            }
        }


        private void GetClients()
        {
            Clients = new BindableCollection<ClientModel>(_data.GetClients());
        }

        public bool CanEditClient
        {
            get
            {
                if (SelectedClient != null)
                {
                    return true;
                }

                return false;
            }
        }

        public void EditClient()
        {
            _eventAggregator.PublishOnUIThread(new SelectedClientEvent(SelectedClient));
            ActivateItem(_editClientVM);
        }

        public void NewClient()
        {
            ActivateItem(_newClientEntryVM);
        }
    }
}
