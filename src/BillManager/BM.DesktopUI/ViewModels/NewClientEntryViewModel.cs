using BM.Library.DataAccess;
using BM.Library.Models;
using Stylet;
using StyletIoC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.DesktopUI.ViewModels
{
    public class NewClientEntryViewModel : Screen
    {
        private readonly IWindowManager _windowManager;
        private readonly IClientData _clientData;
        private readonly IDefaultsData _defaultsData;

        public NewClientEntryViewModel(IWindowManager windowManager, IClientData clientData, IDefaultsData defaultsData)
        {
            DisplayName = "New Entry";

            _windowManager = windowManager;
            _clientData = clientData;
            _defaultsData = defaultsData;
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            LoadDefaultsFromDB();
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set 
            {
                SetAndNotify(ref _name, value);
                NotifyOfPropertyChange(() => CanAdd);
            }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set 
            {
                SetAndNotify(ref _email, value);
                NotifyOfPropertyChange(() => CanAdd);
            }
        }

        private string _hourlyRate;

        public string HourlyRate
        {
            get { return _hourlyRate; }
            set
            {
                SetAndNotify(ref _hourlyRate, value);
                NotifyOfPropertyChange(() => CanAdd);
            }
        }

        private bool _preBill;

        public bool PreBill
        {
            get { return _preBill; }
            set
            {
                SetAndNotify(ref _preBill, value);
                NotifyOfPropertyChange(() => CanAdd);
            }
        }

        private bool _hasCutOff;

        public bool HasCutOff
        {
            get { return _hasCutOff; }
            set
            {
                SetAndNotify(ref _hasCutOff, value);
                NotifyOfPropertyChange(() => CanAdd);
            }
        }

        private string _cutOff;

        public string CutOff
        {
            get { return _cutOff; }
            set
            {
                SetAndNotify(ref _cutOff, value);
                NotifyOfPropertyChange(() => CanAdd);
            }
        }

        private string _minimumHours;

        public string MinimumHours
        {
            get { return _minimumHours; }
            set
            {
                SetAndNotify(ref _minimumHours, value);
                NotifyOfPropertyChange(() => CanAdd);
            }
        }

        private string _billingIncrement;

        public string BillingIncrement
        {
            get { return _billingIncrement; }
            set
            {
                SetAndNotify(ref _billingIncrement, value);
                NotifyOfPropertyChange(() => CanAdd);
            }
        }

        private string _roundUpAfterXMinutes;

        public string RoundUpAfterXMinutes
        {
            get { return _roundUpAfterXMinutes; }
            set
            {
                SetAndNotify(ref _roundUpAfterXMinutes, value);
                NotifyOfPropertyChange(() => CanAdd);
            }
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

        public bool CanAdd
        {
            get
            {
                return ValidateTextBoxes();
            }
        }

        public void Add()
        {
            var model = PopulateClientModel();

            _clientData.InsertClientData(model);

            _windowManager.ShowMessageBox("The client has been added successfuly");
        }

        public void Cancel()
        {
            RequestClose();
        }
    }
}
