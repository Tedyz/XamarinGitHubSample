using Android.Content;
using GenesisAuto.Core.Platform;
using MvvmCross;
using MvvmCross.Platforms.Android;

namespace GenesisAuto.Droid.Platform
{
    public class Device : IDevice
    {
        public void OpenUrl(string url)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
#pragma warning restore CS0618 // Type or member is obsolete
            Intent browserIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(url));

            activity.StartActivity(browserIntent);
        }
    }
}