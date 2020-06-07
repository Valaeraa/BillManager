using BM.DesktopUI.Events;
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

        public EditClientViewModel(IEventAggregator eventAggregator)
        {
            DisplayName = "Edit";

            _eventAggregator = eventAggregator;
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

        private int _cutOff;

        public int CutOff
        {
            get { return _cutOff; }
            set
            {
                SetAndNotify(ref _cutOff, value);
            }
        }

        private double _minimumHours;

        public double MinimumHours
        {
            get { return _minimumHours; }
            set
            {
                SetAndNotify(ref _minimumHours, value);
            }
        }

        private double _billingIncrement;

        public double BillingIncrement
        {
            get { return _billingIncrement; }
            set
            {
                SetAndNotify(ref _billingIncrement, value);
            }
        }

        private int _roundUpAfterXMinutes;

        public int RoundUpAfterXMinutes
        {
            get { return _roundUpAfterXMinutes; }
            set
            {
                SetAndNotify(ref _roundUpAfterXMinutes, value);
            }
        }

        private void LoadSelectedClient(SelectedClientEvent message)
        {
            Name = message.SelectedClient.Name;
            Email = message.SelectedClient.Email;
            HourlyRate = message.SelectedClient.HourlyRate;
            PreBill = (message.SelectedClient.PreBill > 0);
            HasCutOff = (message.SelectedClient.HasCutOff > 0);
            CutOff = message.SelectedClient.CutOff;
            MinimumHours = message.SelectedClient.MinimumHours;
            BillingIncrement = message.SelectedClient.BillingIncrement;
            RoundUpAfterXMinutes = message.SelectedClient.RoundUpAfterXMinutes;
        }

        public bool CanUpdate { get { return true; } }

        public void Update()
        {

        }

        public void Cancel()
        {
            RequestClose();
        }

        public void Handle(SelectedClientEvent message)
        {
            LoadSelectedClient(message);
        }
    }
}
