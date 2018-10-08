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
using GenesisAuto.Core;
using GenesisAuto.Core.Platform;
using GenesisAuto.Droid.CustomBindings;
using GenesisAuto.Droid.Platform;
using MvvmCross;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.ViewModels;
using Square.Picasso;

namespace GenesisAuto.Droid
{
    public class Setup : MvxAndroidSetup<App>
    {


        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            Mvx.RegisterSingleton<IDevice>(new Device());
        }
        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            // Limiting Picasso cash in 30 MB
            Picasso picasso = new Picasso.Builder(ApplicationContext).Downloader(new OkHttpDownloader(ApplicationContext.CacheDir, 30000000)).Build();
            Picasso.SetSingletonInstance(picasso);

            registry.RegisterFactory(new MvxCustomBindingFactory<ImageView>("ImageUrl", (view) => new ImageUrlToLoadBinding(view, Resource.Mipmap.githubicon, Resource.Mipmap.githubicon)));
            registry.RegisterFactory(new MvxCustomBindingFactory<WebView>("SourceHtml", (view) => new WebViewBinding(view)));


            base.FillTargetFactories(registry);
        }
    }
}