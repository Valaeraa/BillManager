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
using System.Windows;
using System.Windows.Input;

namespace BM.DesktopUI.ViewModels
{
    public class DefaultsViewModel : Screen
    {
        private readonly IWindowManager _windowManager;
        private readonly IDefaultsData _data;

        public DefaultsViewModel(IWindowManager windowManager, IDefaultsData data)
        {
            DisplayName = "Defaults";

            _windowManager = windowManager;
            _data = data;

            LoadDefaultsFromDB();
        }

        private string _hourlyRate;

        public string HourlyRate
        {
            get { return _hourlyRate; }
            set 
            {
                SetAndNotify(ref _hourlyRate, value);
                NotifyOfPropertyChange(() => CanUpdateForm);
            }
        }

        private bool _preBill;

        public bool PreBill
        {
            get { return _preBill; }
            set 
            {
                SetAndNotify(ref _preBill, value);
                NotifyOfPropertyChange(() => CanUpdateForm);
            }
        }

        private bool _hasCutOff;

        public bool HasCutOff
        {
            get { return _hasCutOff; }
            set 
            {
                SetAndNotify(ref _hasCutOff, value);
                NotifyOfPropertyChange(() => CanUpdateForm);
            }
        }

        private string _cutOff;

        public string CutOff
        {
            get { return _cutOff; }
            set 
            {
                SetAndNotify(ref _cutOff, value);
                NotifyOfPropertyChange(() => CanUpdateForm);
            }
        }

        private string _minimumHours;

        public string MinimumHours
        {
            get { return _minimumHours; }
            set 
            {
                SetAndNotify(ref _minimumHours, value);
                NotifyOfPropertyChange(() => CanUpdateForm);
            }
        }

        private string _billingIncrement;

        public string BillingIncrement
        {
            get { return _billingIncrement; }
            set 
            {
                SetAndNotify(ref _billingIncrement, value);
                NotifyOfPropertyChange(() => CanUpdateForm);
            }
        }

        private string _roundUpAfterXMinutes;

        public string RoundUpAfterXMinutes
        {
            get { return _roundUpAfterXMinutes; }
            set 
            {
                SetAndNotify(ref _roundUpAfterXMinutes, value);
                NotifyOfPropertyChange(() => CanUpdateForm);
            }
        }

        private void LoadDefaultsFromDB()
        {
            var model = _data.GetDefaultsData();

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

        private void UpdateDatabase(DefaultsModel model)
        {
            _data.DeleteDefaultsData();
            _data.InsertDefaultsData(model);
        }

        private DefaultsModel PopulateDefaultsModel()
        {
            var output = new DefaultsModel
            {
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

        public bool CanUpdateForm
        {
            get { return ValidateTextBoxes(); }
        }

        public void UpdateForm()
        {
            var model = PopulateDefaultsModel();

            UpdateDatabase(model);

            _windowManager.ShowMessageBox("The form has been updated successfuly");
        }
    }
}
