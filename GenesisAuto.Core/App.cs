using GenesisAuto.Core.Services;
using GenesisAuto.Core.ViewModels;
using MvvmCross;
using MvvmCross.Plugin;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GenesisAuto.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.ConstructAndRegisterSingleton<IApis, Apis>();

            RegisterAppStart<HomeViewModel>();
        }
    }
}
