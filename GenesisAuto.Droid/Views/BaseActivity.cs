using Android.OS;
using MvvmCross.Droid.Support.V7.AppCompat;
using Android.Support.V7.Widget;
using MvvmCross.ViewModels;

namespace GenesisAuto.Droid.Views
{
    public abstract class BaseActivity : MvxAppCompatActivity
    {
        protected abstract int ResourceLayoutId
        {
            get;
        }

        protected Toolbar Toolbar { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
           
            SetContentView(ResourceLayoutId);
            PrepareToolbar();
        }

        protected override void OnResume()
        {
            base.OnResume();
            OverridePendingTransition(Resource.Animation.abc_fade_in, Resource.Animation.abc_fade_out);
        }

        protected override void OnPause()
        {
            base.OnPause();
            OverridePendingTransition(Resource.Animation.abc_fade_in, Resource.Animation.abc_fade_out);
        }

        private void PrepareToolbar()
        {
            this.Toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            if (this.Toolbar != null)
            {
                SetSupportActionBar(this.Toolbar);
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetHomeButtonEnabled(true);
            }
        }
    }

    public abstract class BaseActivity<TViewModel> : BaseActivity where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}