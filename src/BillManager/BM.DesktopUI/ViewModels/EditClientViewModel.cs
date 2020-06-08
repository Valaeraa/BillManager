using BM.DesktopUI.Events;
using BM.Library.DataAccess;
using BM.Library.Models;
using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.DesktopUI.ViewModels
{
    public class EditClientViewModel : Screen, IHandle<SelectedClientEvent>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;
        private readonly IClientData _clientData;

        public EditClientViewModel(IEventAggregator eventAggregator, IWindowManager windowManager, IClientData clientData)
        {
            DisplayName = "Edit";

            _eventAggregator = eventAggregator;
            _windowManager = windowManager;
            _clientData = clientData;
            _eventAggregator.Subscribe(this);
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
                NotifyOfPropertyChange(() => CanUpdate);
            }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set 
            {
                SetAndNotify(ref _email, value);
                NotifyOfPropertyChange(() => CanUpdate);
            }
        }

        private string _hourlyRate;

        public string HourlyRate
        {
            get { return _hourlyRate; }
            set 
            {
                SetAndNotify(ref _hourlyRate, value);
                NotifyOfPropertyChange(() => CanUpdate);
            }
        }

        private bool _preBill;

        public bool PreBill
        {
            get { return _preBill; }
            set
            {
                SetAndNotify(ref _preBill, value);
                NotifyOfPropertyChange(() => CanUpdate);
            }
        }

        private bool _hasCutOff;

        public bool HasCutOff
        {
            get { return _hasCutOff; }
            set
            {
                SetAndNotify(ref _hasCutOff, value);
                NotifyOfPropertyChange(() => CanUpdate);
            }
        }

        private string _cutOff;

        public string CutOff
        {
            get { return _cutOff; }
            set
            {
                SetAndNotify(ref _cutOff, value);
                NotifyOfPropertyChange(() => CanUpdate);
            }
        }

        private string _minimumHours;

        public string MinimumHours
        {
            get { return _minimumHours; }
            set
            {
                SetAndNotify(ref _minimumHours, value);
                NotifyOfPropertyChange(() => CanUpdate);
            }
        }

        private string _billingIncrement;

        public string BillingIncrement
        {
            get { return _billingIncrement; }
            set
            {
                SetAndNotify(ref _billingIncrement, value);
                NotifyOfPropertyChange(() => CanUpdate);
            }
        }

        private string _roundUpAfterXMinutes;

        public string RoundUpAfterXMinutes
        {
            get { return _roundUpAfterXMinutes; }
            set
            {
                SetAndNotify(ref _roundUpAfterXMinutes, value);
                NotifyOfPropertyChange(() => CanUpdate);
            }
        }

        private ClientModel PopulateClientModel()
        {
            var output = new ClientModel
            {
                Id = Id,
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

        public bool CanUpdate { get { return ValidateTextBoxes(); } }

        public void Update()
        {
            var model = PopulateClientModel();

            _clientData.UpdateClientData(model);

            _windowManager.ShowMessageBox("The client has been updated successfuly");
        }

        public void Cancel()
        {
            RequestClose();
        }

        public void Handle(SelectedClientEvent message)
        {
            LoadSelectedClient(message);
        }

        private void LoadSelectedClient(SelectedClientEvent message)
        {
            Id = message.SelectedClient.Id;
            Name = message.SelectedClient.Name;
            Email = message.SelectedClient.Email;
            HourlyRate = message.SelectedClient.HourlyRate.ToString();
            PreBill = (message.SelectedClient.PreBill > 0);
            HasCutOff = (message.SelectedClient.HasCutOff > 0);
            CutOff = message.SelectedClient.CutOff.ToString();
            MinimumHours = message.SelectedClient.MinimumHours.ToString();
            BillingIncrement = message.SelectedClient.BillingIncrement.ToString();
            RoundUpAfterXMinutes = message.SelectedClient.RoundUpAfterXMinutes.ToString();
        }
    }
}
