using Android.Webkit;
using Android.Widget;
using ArcTouch.Core;
using ArcTouch.Droid.CustomBindings;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Platforms.Android.Core;
using Square.Picasso;

namespace ArcTouch.Droid
{
    public class Setup : MvxAndroidSetup<App>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();
        }
        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            // Limiting Picasso cash in 30 MB
            Picasso picasso = new Picasso.Builder(ApplicationContext).Downloader(new OkHttpDownloader(ApplicationContext.CacheDir, 30000000)).Build();
            Picasso.SetSingletonInstance(picasso);

            registry.RegisterFactory(new MvxCustomBindingFactory<ImageView>("ImageUrl", (view) => new ImageUrlToLoadBinding(view, Resource.Drawable.ic_launcher, Resource.Drawable.ic_launcher)));


            base.FillTargetFactories(registry);
        }
    }
}