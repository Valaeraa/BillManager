using BM.DesktopUI.Views;
using System;
using System.Collections.Generic;
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

namespace BM.DesktopUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ContentControl ActivateItem { get; set; } = new ContentControl();

        private MainView _mainView = new MainView();
        private ClientView _clientView = new ClientView();
        private PaymentView _paymentView = new PaymentView();
        private WorkView _workView = new WorkView();
        private DefaultsView _defaultsView = new DefaultsView();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TabViewer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainTab.IsSelected)
            {
                MainTab.Content = ActivateItem.Content = _mainView;
            }

            if (ClientTab.IsSelected)
            {
                ClientTab.Content = ActivateItem.Content = _clientView;
            }

            if (PaymentTab.IsSelected)
            {
                PaymentTab.Content = ActivateItem.Content = _paymentView;
            }

            if (WorkTab.IsSelected)
            {
                WorkTab.Content = ActivateItem.Content = _workView;
            }

            if (DefaultsTab.IsSelected)
            {
                DefaultsTab.Content = ActivateItem.Content = _defaultsView;
            }
        }
    }
}
