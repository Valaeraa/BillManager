using BM.Library.DataAccess;
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
        private readonly IDefaultsData _data;

        public NewClientEntryViewModel(IDefaultsData data)
        {
            DisplayName = "New Entry";

            _data = data;

            //LoadDefaultsFromDB();
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            LoadDefaultsFromDB();
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

        public bool CanSubmit
        {
            get
            {
                // TODO: Implement logic for submiting
                return true;
            }
        }

        public void Submit()
        {

        }

        public void Cancel()
        {
            RequestClose();
        }
    }
}
