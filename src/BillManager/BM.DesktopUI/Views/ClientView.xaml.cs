using BM.Library.DataAccess;
using BM.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BM.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for ClientView.xaml
    /// </summary>
    public partial class ClientView : UserControl
    {
        private ClientData ClientData { get; set; } = new ClientData();
        private ObservableCollection<ClientModel> Clients { get; set; }


        public ClientView()
        {
            InitializeComponent();

            GetInitialClients();
            InitializeClientDropDown();
        }

        private void GetInitialClients()
        {
            var getClients = ClientData.GetClients();
            Clients = new ObservableCollection<ClientModel>(getClients);
        }

        private void InitializeClientDropDown()
        {
            ClientDropDown.ItemsSource = Clients;
            //ClientDropDown.DisplayMemberPath = "Name";
            //ClientDropDown.SelectedValuePath = "Id";
            ClientDropDown.SelectedItem = Clients.FirstOrDefault();

            //var id = ClientDropDown.SelectedValue;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            ActivateView.Content = new ClientEditView();
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            ActivateView.Content = new ClientNewEntryView();
        }

        private void SubmitForm_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ClearForm_Click(object sender, RoutedEventArgs e)
        {
            ActivateView.Content = null;
        }
    }
}
