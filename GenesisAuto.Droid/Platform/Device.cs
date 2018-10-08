using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GenesisAuto.Core.Platform;
using MvvmCross;
using MvvmCross.Platforms.Android;

namespace GenesisAuto.Droid.Platform
{
    public class Device : IDevice
    {
        public void OpenUrl(string url)
        {
            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            Intent browserIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(url));

            activity.StartActivity(browserIntent);
        }
    }
}