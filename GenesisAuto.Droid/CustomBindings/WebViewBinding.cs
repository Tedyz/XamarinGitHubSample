using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using MvvmCross.Platforms.Android.Binding.Target;
using Square.Picasso;

namespace GenesisAuto.Droid.CustomBindings
{
    public class WebViewBinding : MvxAndroidTargetBinding
    {

        public WebViewBinding(View target) : base(target)
        {

        }

        public int PlaceHolderToUse { get; set; }

        protected override void SetValueImpl(object target, object value)
        {
            if (value == null)
                return;

            var webView = (WebView)target;
            webView.Settings.JavaScriptEnabled = true;
            webView.LoadData(value as string, "text/html", "UTF-8");
        }

        public override Type TargetType => typeof(string);
    }
}