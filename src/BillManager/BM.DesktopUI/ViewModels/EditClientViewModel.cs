using BM.DesktopUI.Events;
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
        public EditClientViewModel()
        {
            DisplayName = "Edit";
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
            set { SetAndNotify(ref _name, value); }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { SetAndNotify(ref _email, value); }
        }

        private double _hourlyRate;

        public double HourlyRate
        {
            get { return _hourlyRate; }
            set { SetAndNotify(ref _hourlyRate, value); }
        }

        private bool _preBill;

        public bool PreBill
        {
            get { return _preBill; }
            set
            {
                SetAndNotify(ref _preBill, value);
            }
        }

        private bool _hasCutOff;

        public bool HasCutOff
        {
            get { return _hasCutOff; }
            set
            {
                SetAndNotify(ref _hasCutOff, value);
            }
        }

        private string _cutOff;

        public string CutOff
        {
            get { return _cutOff; }
            set
            {
                SetAndNotify(ref _cutOff, value);
            }
        }

        private string _minimumHours;

        public string MinimumHours
        {
            get { return _minimumHours; }
            set
            {
                SetAndNotify(ref _minimumHours, value);
            }
        }

        private string _billingIncrement;

        public string BillingIncrement
        {
            get { return _billingIncrement; }
            set
            {
                SetAndNotify(ref _billingIncrement, value);
            }
        }

        private string _roundUpAfterXMinutes;

        public string RoundUpAfterXMinutes
        {
            get { return _roundUpAfterXMinutes; }
            set
            {
                SetAndNotify(ref _roundUpAfterXMinutes, value);
            }
        }

        public void Cancel()
        {
            RequestClose();
        }

        public void Handle(SelectedClientEvent message)
        {
            throw new NotImplementedException();
        }
    }
}
