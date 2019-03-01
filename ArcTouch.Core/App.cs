using ArcTouch.Core.Services;
using ArcTouch.Core.ViewModels;
using MvvmCross;
using MvvmCross.ViewModels;

namespace ArcTouch.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {

            Mvx.IoCProvider.RegisterSingleton<IApis>(new Apis());

            RegisterAppStart<HomeViewModel>();
        }
    }
}
