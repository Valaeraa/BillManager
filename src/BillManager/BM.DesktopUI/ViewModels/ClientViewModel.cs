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
using System.Windows;
using System.Windows.Controls;

namespace BM.DesktopUI.ViewModels
{
    public class ClientViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly IWindowManager _windowManager;
        private readonly IClientData _clientData;
        private readonly IDefaultsData _defaultsData;
        private bool _isNewClient = false;

        public ClientViewModel(IWindowManager windowManager, IClientData clientData, IDefaultsData defaultsData)
        {
            DisplayName = "Clients";

            _windowManager = windowManager;
            _clientData = clientData;
            _defaultsData = defaultsData;

            GetClients();

            SetFormVisibily(Visibility.Visible, Visibility.Collapsed);
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

        private Visibility _clientStackPanel;

        public Visibility ClientStackPanel
        {
            get { return _clientStackPanel; }
            set { SetAndNotify(ref _clientStackPanel, value); }
        }

        private Visibility _formStackPanel;

        public Visibility FormStackPanel
        {
            get { return _formStackPanel; }
            set { SetAndNotify(ref _formStackPanel, value); }
        }

        private Visibility _title;

        public Visibility Title
        {
            get { return _title; }
            set { SetAndNotify(ref _title, value); }
        }

        private string _formTitle;

        public string FormTitle
        {
            get { return _formTitle; }
            set { SetAndNotify(ref _formTitle, value); }
        }

        private int _id;

        public int Id
        {
            get { return _id; }
            set { SetAndNotify(ref _id, value); }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                SetAndNotify(ref _name, value);
                NotifyOfPropertyChange(() => CanSubmit);
            }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                SetAndNotify(ref _email, value);
                NotifyOfPropertyChange(() => CanSubmit);
            }
        }

        private string _hourlyRate;

        public string HourlyRate
        {
            get { return _hourlyRate; }
            set
            {
                SetAndNotify(ref _hourlyRate, value);
                NotifyOfPropertyChange(() => CanSubmit);
            }
        }

        private bool _preBill;

        public bool PreBill
        {
            get { return _preBill; }
            set
            {
                SetAndNotify(ref _preBill, value);
                NotifyOfPropertyChange(() => CanSubmit);
            }
        }

        private bool _hasCutOff;

        public bool HasCutOff
        {
            get { return _hasCutOff; }
            set
            {
                SetAndNotify(ref _hasCutOff, value);
                NotifyOfPropertyChange(() => CanSubmit);
            }
        }

        private string _cutOff;

        public string CutOff
        {
            get { return _cutOff; }
            set
            {
                SetAndNotify(ref _cutOff, value);
                NotifyOfPropertyChange(() => CanSubmit);
            }
        }

        private string _minimumHours;

        public string MinimumHours
        {
            get { return _minimumHours; }
            set
            {
                SetAndNotify(ref _minimumHours, value);
                NotifyOfPropertyChange(() => CanSubmit);
            }
        }

        private string _billingIncrement;

        public string BillingIncrement
        {
            get { return _billingIncrement; }
            set
            {
                SetAndNotify(ref _billingIncrement, value);
                NotifyOfPropertyChange(() => CanSubmit);
            }
        }

        private string _roundUpAfterXMinutes;

        public string RoundUpAfterXMinutes
        {
            get { return _roundUpAfterXMinutes; }
            set
            {
                SetAndNotify(ref _roundUpAfterXMinutes, value);
                NotifyOfPropertyChange(() => CanSubmit);
            }
        }

        private string _buttonName;

        public string ButtonName
        {
            get { return _buttonName; }
            set { SetAndNotify(ref _buttonName, value); }
        }


        private void GetClients()
        {
            Clients = new BindableCollection<ClientModel>(_clientData.GetClients());
        }

        private void LoadSelectedClient()
        {
            Id = SelectedClient.Id;
            Name = SelectedClient.Name;
            Email = SelectedClient.Email;
            HourlyRate = SelectedClient.HourlyRate.ToString();
            PreBill = (SelectedClient.PreBill > 0);
            HasCutOff = (SelectedClient.HasCutOff > 0);
            CutOff = SelectedClient.CutOff.ToString();
            MinimumHours = SelectedClient.MinimumHours.ToString();
            BillingIncrement = SelectedClient.BillingIncrement.ToString();
            RoundUpAfterXMinutes = SelectedClient.RoundUpAfterXMinutes.ToString();
        }

        private void LoadDefaultsFromDB()
        {
            var model = _defaultsData.GetDefaultsData();

            if (model != null)
            {
                HourlyRate = model.HourlyRate.ToString();
                PreBill = (model.HourlyRate > 0);
                HasCutOff = (model.HasCutOff > 0);
                CutOff = model.CutOff.ToString();
                MinimumHours = model.MinimumHours.ToString();
                BillingIncrement = model.BillingIncrement.ToString();
                RoundUpAfterXMinutes = model.RoundUpAfterXMinutes.ToString();
            }
        }

        private void ClearFields()
        {
            Name = "";
            Email = "";
            HourlyRate = "";
            PreBill = false;
            HasCutOff = false;
            CutOff = "";
            MinimumHours = "";
            BillingIncrement = "";
            RoundUpAfterXMinutes = "";
        }

        private void SetFormVisibily(Visibility client, Visibility form)
        {
            Title = client;
            ClientStackPanel = client;
            FormStackPanel = form;
        }

        private void SetFormName(string title, string button)
        {
            FormTitle = title;
            ButtonName = button;
        }

        private ClientModel PopulateClientModel()
        {
            var output = new ClientModel
            {
                Name = Name,
                Email = Email,
                PreBill = PreBill ? 1 : 0,
                HasCutOff = HasCutOff ? 1 : 0,
                HourlyRate = double.Parse(HourlyRate),
                CutOff = int.Parse(CutOff),
                MinimumHours = double.Parse(MinimumHours),
                BillingIncrement = double.Parse(BillingIncrement),
                RoundUpAfterXMinutes = int.Parse(RoundUpAfterXMinutes)
            };

            if (_isNewClient == false)
            {

                output.Id = Id;
            }

            return output;
        }

        private bool ValidateTextBoxes()
        {
            var output = true;

            // Name
            if (string.IsNullOrWhiteSpace(Name))
            {
                return false;
            }

            // Email
            if (string.IsNullOrWhiteSpace(Email))
            {
                return false;
            }

            // HourlyRate
            var isHourlyRateValid = double.TryParse(HourlyRate, out double hourlyRate);

            if (isHourlyRateValid == false)
            {
                return false;
            }

            output = (hourlyRate < 0) ? false : true;

            // CutOff
            var isCutOffValid = int.TryParse(CutOff, out int cutOff);

            if (isCutOffValid == false)
            {
                return false;
            }

            output = (cutOff < 0) ? false : true;

            // MinimumHours
            var isMinimumHoursValid = double.TryParse(MinimumHours, out double minimumHours);

            if (isMinimumHoursValid == false)
            {
                return false;
            }

            output = (minimumHours < 0) ? false : true;

            // BillingIncrement
            var isBillingIncrementValid = double.TryParse(BillingIncrement, out double billingIncrement);

            if (isBillingIncrementValid == false)
            {
                return false;
            }

            output = (billingIncrement < 0) ? false : true;

            // RoundUpAfterXMinutes
            var isRoundUpAfterXMinutesValid = int.TryParse(RoundUpAfterXMinutes, out int roundUpAfterXMinutes);

            if (isRoundUpAfterXMinutesValid == false)
            {
                return false;
            }

            output = (roundUpAfterXMinutes < 0) ? false : true;

            return output;
        }

        public bool CanEditClient
        {
            get
            {
                return (SelectedClient != null) ? true : false;
            }
        }

        public void EditClient()
        {
            _isNewClient = false;

            ClearFields();
            SetFormVisibily(Visibility.Collapsed, Visibility.Visible);
            SetFormName("Edit Client", "Update");
            LoadSelectedClient();
        }

        public void NewClient()
        {
            _isNewClient = true;

            ClearFields();
            SetFormVisibily(Visibility.Collapsed, Visibility.Visible);
            SetFormName("New Client", "Add");
            LoadDefaultsFromDB();
        }

        public bool CanSubmit { get { return ValidateTextBoxes(); } }

        public void Submit()
        {
            if (_isNewClient == true)
            {
                // Add a new client
                var model = PopulateClientModel();

                _clientData.InsertClientData(model);
                _windowManager.ShowMessageBox("The client has been added successfuly");
            }
            else
            {
                // Update selected client
                var model = PopulateClientModel();

                _clientData.UpdateClientData(model);
                _windowManager.ShowMessageBox("The client has been updated successfuly");
            }

            ClearFields();
            SetFormVisibily(Visibility.Visible, Visibility.Collapsed);
            GetClients();
        }

        public void Close()
        {
            ClearFields();
            SetFormVisibily(Visibility.Visible, Visibility.Collapsed);
        }
    }
}
