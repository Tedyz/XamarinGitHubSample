using System;
using Android.App;
using Android.Runtime;
using MvvmCross.Platforms.Android.Views;
using GenesisAuto.Core;


namespace GenesisAuto.Droid
{
    [Application]
    public class MainApplication : MvxAndroidApplication<Setup, App>
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
    }
}