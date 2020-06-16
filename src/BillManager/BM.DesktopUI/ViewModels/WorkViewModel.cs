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
    public class WorkViewModel : Screen
    {
        private readonly IWindowManager _windowManager;
        private readonly IWorkData _workData;
        private readonly IClientData _clientData;
        private readonly IPaymentData _paymentData;

        public WorkViewModel(IWindowManager windowManager, IWorkData workData,
            IClientData clientData, IPaymentData paymentData)
        {
            DisplayName = "Work";

            _windowManager = windowManager;
            _workData = workData;
            _clientData = clientData;
            _paymentData = paymentData;

            GetClients();

            InitializeFormVisibility();
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

        private BindableCollection<WorkModel> _dates;

        public BindableCollection<WorkModel> Dates
        {
            get { return _dates; }
            set { SetAndNotify(ref _dates, value); }
        }

        private WorkModel _selectedDate;

        public WorkModel SelectedDate
        {
            get { return _selectedDate; }
            set { SetAndNotify(ref _selectedDate, value); }
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
            }
        }

        private Visibility _clientStackPanel;

        public Visibility ClientStackPanel
        {
            get { return _clientStackPanel; }
            set { SetAndNotify(ref _clientStackPanel, value); }
        }

        private Visibility _dateStackPanel;

        public Visibility DateStackPanel
        {
            get { return _dateStackPanel; }
            set { SetAndNotify(ref _dateStackPanel, value); }
        }

        private Visibility _formStackPanel;

        public Visibility FormStackPanel
        {
            get { return _formStackPanel; }
            set { SetAndNotify(ref _formStackPanel, value); }
        }

        private Visibility _paymentStackPanel;

        public Visibility PaymentStackPanel
        {
            get { return _paymentStackPanel; }
            set { SetAndNotify(ref _paymentStackPanel, value); }
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

        private string _title;

        public string Title
        {
            get { return _title; }
            set 
            { 
                SetAndNotify(ref _title, value);
                NotifyOfPropertyChange(() => CanSubmit);
            }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set 
            { 
                SetAndNotify(ref _description, value);
                NotifyOfPropertyChange(() => CanSubmit);
            }
        }

        private bool _paid;

        public bool Paid
        {
            get { return _paid; }
            set { SetAndNotify(ref _paid, value); }
        }

        private void GetClients()
        {
            Clients = new BindableCollection<ClientModel>(_clientData.GetClients());
        }

        private void GetDates()
        {
            Dates = new BindableCollection<WorkModel>(_workData.GetWorkData(SelectedClient.Id));
        }

        private void GetPayments()
        {
            Payments = new BindableCollection<PaymentModel>(_paymentData.GetPayments(SelectedClient.Id));
        }

        public void ClientSelectionChanged()
        {
            DateStackPanel = Visibility.Visible;

            GetDates();
            GetPayments();
        }

        public void DateSelectionChanged()
        {
            if (SelectedDate == null)
            {
                return;
            }

            FormStackPanel = Visibility.Visible;

            Hours = SelectedDate.Hours.ToString();
            Title = SelectedDate.Title;
            Description = SelectedDate.Description;
            Paid = (SelectedDate.Paid > 0);

            if (SelectedDate.Paid > 0)
            {
                PaymentStackPanel = Visibility.Visible;

                var selectedPayment = Payments.Where(x => x.Id == SelectedDate.PaymentId).FirstOrDefault();

                if (selectedPayment != null)
                {
                    SelectedPayment = selectedPayment;
                }
            }
            else
            {
                PaymentStackPanel = Visibility.Collapsed;
            }
        }

        public void PaidCheckbox()
        {
            if (Paid == true)
            {
                PaymentStackPanel = Visibility.Visible;
            }
            else
            {
                PaymentStackPanel = Visibility.Collapsed;
            }
        }

        private void InitializeFormVisibility()
        {
            DateStackPanel = Visibility.Collapsed;
            FormStackPanel = Visibility.Collapsed;
            PaymentStackPanel = Visibility.Collapsed;
        }

        private bool ValidateTextBoxes()
        {
            var output = true;

            // Hours
            var isHoursValid = double.TryParse(Hours, out double hours);

            if (isHoursValid == false)
            {
                return false;
            }

            output = (hours < 0) ? false : true;

            if (string.IsNullOrWhiteSpace(Title))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                return false;
            }

            return output;
        }

        private WorkModel PopulateWorkModel()
        {
            var payment = SelectedPayment;
            int? paymentId;

            if (payment != null && Paid == true)
            {
                paymentId = payment.Id;
            }
            else
            {
                paymentId = null;
            }

            var output = new WorkModel
            {
                Id = SelectedDate.Id,
                Hours = double.Parse(Hours),
                Title = Title,
                Description = Description,
                Paid = Paid ? 1 : 0,
                PaymentId = paymentId
            };

            return output;
        }

        public bool CanSubmit { get { return ValidateTextBoxes(); } }

        public void Submit()
        {
            var model = PopulateWorkModel();

            _workData.UpdateWorkData(model);

            SelectedDate = model;

            _windowManager.ShowMessageBox("The work done has been updated successfuly");
        }
    }
}
