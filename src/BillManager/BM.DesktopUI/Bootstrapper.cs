using BM.DesktopUI.ViewModels;
using BM.Library.DataAccess;
using Stylet;
using StyletIoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.DesktopUI
{
    public class Bootstrapper : Bootstrapper<ShellViewModel>
    {
        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            base.ConfigureIoC(builder);

            builder.Bind<IDefaultsData>().To<DefaultsData>();
            builder.Bind<IClientData>().To<ClientData>();
        }
    }
}
