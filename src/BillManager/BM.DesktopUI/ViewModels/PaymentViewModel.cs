using BM.Library.DataAccess;
using BM.Library.Models;
using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BM.DesktopUI.ViewModels
{
    public class PaymentViewModel : Screen
    {
        private readonly IWindowManager _windowManager;
        private readonly IPaymentData _paymentData;
        private readonly IClientData _clientData;
        private bool _isNewEntry = true;

        public PaymentViewModel(IWindowManager windowManager, IPaymentData paymentData, IClientData clientData)
        {
            DisplayName = "Payment";

            _windowManager = windowManager;
            _paymentData = paymentData;
            _clientData = clientData;

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
            }
        }

        private BindableCollection<PaymentModel> _payments = new BindableCollection<PaymentModel>();

        public BindableCollection<PaymentModel> Payments
        {
            get { return _payments; }
            set { SetAndNotify(ref _payments, value); }
        }

        private PaymentModel _selectedPayment;

        public PaymentModel SelectedPayment
        {
            get { return _selectedPayment; }
            set 
            {
                SetAndNotify(ref _selectedPayment, value);
                NotifyOfPropertyChange(() => CanEditPayment);
            }
        }

        private Visibility _clientStackPanel;

        public Visibility ClientStackPanel
        {
            get { return _clientStackPanel; }
            set { SetAndNotify(ref _clientStackPanel, value); }
        }

        private Visibility _paymentStackPanel = Visibility.Collapsed;

        public Visibility PaymentStackPanal
        {
            get { return _paymentStackPanel; }
            set { SetAndNotify(ref _paymentStackPanel, value); }
        }

        private Visibility _formStackPanal = Visibility.Collapsed;

        public Visibility FormStackPanal
        {
            get { return _formStackPanal; }
            set { SetAndNotify(ref _formStackPanal, value); }
        }

        private string _formTitle;

        public string FormTitle
        {
            get { return _formTitle; }
            set { SetAndNotify(ref _formTitle, value); }
        }

        private string _buttonName;

        public string ButtonName
        {
            get { return _buttonName; }
            set { SetAndNotify(ref _buttonName, value); }
        }

        private string _amount;

        public string Amount
        {
            get { return _amount; }
            set 
            {
                SetAndNotify(ref _amount, value);
                NotifyOfPropertyChange(() => CanSubmit);
            }
        }

        private string _hours;

        public string Hours
        {
            get { return _hours; }
            set 
            {
                SetAndNotify(ref _hours, value);
                NotifyOfPropertyChange(() => CanSubmit);
            }
        }

        private void GetClients()
        {
            Clients = new BindableCollection<ClientModel>(_clientData.GetClients());
        }

        private void GetPayments()
        {
            Payments = new BindableCollection<PaymentModel>(_paymentData.GetPayments(SelectedClient.Id));
        }

        private void ClearFields()
        {
            Amount = "";
            Hours = "";
        }

        private void SetFormName(string title, string button)
        {
            FormTitle = title;
            ButtonName = button;
        }

        private bool ValidateTextBoxes()
       {
            var output = true;

            // Amount
            var isAmountValid = double.TryParse(Amount, out double amount);

            if (isAmountValid == false)
            {
                return false;
            }

            output = (amount < 0) ? false : true;


            // Hours
            var isHoursValid = double.TryParse(Hours, out double hours);

            if (isHoursValid == false)
            {
                return false;
            }

            output = (hours < 0) ? false : true;

            return output;
        }

        private PaymentModel PopulatePaymentModel()
        {
            var output = new PaymentModel
            {
                ClientId = SelectedClient.Id,
                Hours = double.Parse(Hours),
                Amount = double.Parse(Amount)
            };

            if (_isNewEntry == false)
            {
                output.Id = SelectedPayment.Id;
            }

            return output;
        }

        public void ClientSelectionChanged()
        {
            PaymentStackPanal = Visibility.Visible;

            GetPayments();
        }

        public bool CanEditPayment { get { return (SelectedClient != null); } }

        public void EditPayment()
        {
            _isNewEntry = false;

            ClearFields();
            SetFormName("Edit Payment", "Update");

            FormStackPanal = Visibility.Visible;

            Amount = SelectedPayment.Amount.ToString();
            Hours = SelectedPayment.Hours.ToString();
        }

        public void NewPayment()
        {
            _isNewEntry = true;

            ClearFields();
            SetFormName("New Payment", "Add");

            FormStackPanal = Visibility.Visible;
        }

        public bool CanSubmit { get { return ValidateTextBoxes(); } }

        public void Submit()
        {
            if (_isNewEntry == true)
            {
                // Add new payment entry
                var model = PopulatePaymentModel();

                _paymentData.InsertPaymentData(model);

                _windowManager.ShowMessageBox("The payment has been added successfuly");
            }
            else
            {
                // Update selected payment
                var model = PopulatePaymentModel();

                _paymentData.UpdatePaymentData(model);

                _windowManager.ShowMessageBox("The payment has been updated successfuly");
            }

            GetPayments();
            ClearFields();

            FormStackPanal = Visibility.Collapsed;
        }

        public void Close()
        {
            ClearFields();

            FormStackPanal = Visibility.Collapsed;
        }
    }
}
