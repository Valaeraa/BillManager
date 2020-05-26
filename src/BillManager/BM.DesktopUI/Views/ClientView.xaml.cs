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
        private ClientData _clientData { get; set; } = new ClientData();
        private ObservableCollection<ClientModel> _clients { get; set; }


        public ClientView()
        {
            InitializeComponent();

            GetInitialClients();
            InitializeClientDropDown();
        }

        private void GetInitialClients()
        {
            var getClients = _clientData.GetClients();
            _clients = new ObservableCollection<ClientModel>(getClients);
        }

        private void InitializeClientDropDown()
        {
            ClientDropDown.ItemsSource = _clients;
            //ClientDropDown.DisplayMemberPath = "Name";
            //ClientDropDown.SelectedValuePath = "Id";
            ClientDropDown.SelectedItem = _clients.FirstOrDefault();

            //var id = ClientDropDown.SelectedValue;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SubmitForm_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ClearForm_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
