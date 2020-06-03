using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.DesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive
    {
        public ShellViewModel(MainViewModel mainVM, ClientViewModel clientVM, PaymentViewModel paymentVM,
            WorkViewModel workVM, DefaultsViewModel defaultsVM)
        {
            Items.Add(mainVM);
            Items.Add(clientVM);
            Items.Add(paymentVM);
            Items.Add(workVM);
            Items.Add(defaultsVM);

            ActiveItem = mainVM;
        }
    }
}
