using GenesisAuto.Core.Services;
using GenesisAuto.Core.ViewModels;
using MvvmCross;
using MvvmCross.ViewModels;

namespace GenesisAuto.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            Mvx.ConstructAndRegisterSingleton<IApis, Apis>();
#pragma warning restore CS0618 // Type or member is obsolete

            RegisterAppStart<HomeViewModel>();
        }
    }
}
