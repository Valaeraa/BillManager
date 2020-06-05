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

        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetAndNotify(ref _name, value); }
        }

        private double _hourlyRate;

        public double HourlyRate
        {
            get { return _hourlyRate; }
            set { SetAndNotify(ref _hourlyRate, value); }
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
