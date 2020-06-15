using BM.Library.DataAccess;
using BM.Library.Models;
using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BM.DesktopUI.ViewModels
{
    public class MainViewModel : Screen
    {
        private readonly IWindowManager _windowManager;
        private readonly IClientData _clientData;
        private readonly IWorkData _workData;

        private bool IsTimerRunning { get; set; } = false;
        private DateTime StartTimer { get; set; }

        public MainViewModel(IWindowManager windowManager, IClientData clientData, IWorkData workData)
        {
            DisplayName = "Main";

            _windowManager = windowManager;
            _clientData = clientData;
            _workData = workData;

            GetClients();

            SetTimerProperties();
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
                NotifyOfPropertyChange(() => CanTimer);
                NotifyOfPropertyChange(() => CanSubmit);
            }
        }

        private string _timerText;

        public string TimerText
        {
            get { return _timerText; }
            set { SetAndNotify(ref _timerText, value); }
        }

        private Brush _timerColor;

        public Brush TimerColor
        {
            get { return _timerColor; }
            set { SetAndNotify(ref _timerColor, value); }
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

        private void GetClients()
        {
            Clients = new BindableCollection<ClientModel>(_clientData.GetClients());
        }

        private WorkModel PopulateWorkModel()
        {
            var output = new WorkModel
            {
                ClientId = SelectedClient.Id,
                Hours = double.Parse(Hours),
                Title = Title,
                Description = Description,
            };

            return output;
        }

        private void ClearFields()
        {
            Hours = "";
            Title = "";
            Description = "";
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

        private void SetTimerProperties()
        {
            TimerText = "Start Timer";
            TimerColor = Brushes.Green;
        }

        private void CalculateHours(double minutes)
        {
            var total = 0.0;
            var tempMinutes = minutes;
            var billingMinutes = (SelectedClient.BillingIncrement * 60);

            while (tempMinutes >= billingMinutes)
            {
                total += SelectedClient.BillingIncrement;
                tempMinutes -= billingMinutes;
            }

            if (tempMinutes >= SelectedClient.RoundUpAfterXMinutes)
            {
                total += SelectedClient.BillingIncrement;
            }

            Hours = total.ToString();
        }

        public bool CanTimer { get { return (SelectedClient != null); } }

        public void Timer()
        {
            if (IsTimerRunning == true)
            {
                var minutes = DateTime.Now.Subtract(StartTimer).TotalMinutes;

                CalculateHours(minutes);

                IsTimerRunning = false;

                TimerText = "Start Timer";
                TimerColor = Brushes.Green;
            }
            else
            {
                StartTimer = DateTime.Now;

                IsTimerRunning = true;

                TimerText = "Stop Timer";
                TimerColor = Brushes.Red;
            }
        }

        public bool CanSubmit { get { return (ValidateTextBoxes() && SelectedClient != null); } }

        public void Submit()
        {
            var model = PopulateWorkModel();

            _workData.InsertWorkData(model);

            _windowManager.ShowMessageBox("The work done has been added successfuly");

            ClearFields();
        }
    }
}
