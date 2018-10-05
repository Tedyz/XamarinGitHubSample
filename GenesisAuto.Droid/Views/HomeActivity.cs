using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using GenesisAuto.Core.ViewModels;
using MvvmCross.Platforms.Android.Views;

namespace GenesisAuto.Droid.Views
{
    [Activity(Label = "HomeActivity", MainLauncher = true) ]
    public class HomeActivity : BaseActivity<HomeActivity, HomeViewModel>
    {
        protected override int ResourceLayoutId => Resource.Layout.drawer_layout;

        private DrawerLayout MyDrawerLayout { get; set; }
        private ActionBarDrawerToggle MyToggle { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Title = Resources.GetString(Resource.String.title_home);

            MyDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            MyToggle = new ActionBarDrawerToggle(this, MyDrawerLayout, Resource.String.open_drawer, Resource.String.close_drawer);
            MyDrawerLayout.AddDrawerListener(MyToggle);
            MyToggle.SyncState();
            
            // SupportActionBar.SetHomeButtonEnabled(false);

            // Create your application here
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (MyToggle.OnOptionsItemSelected(item))
            {
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}