using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Support.V7.Widget;
using GenesisAuto.Core.ViewModels;
using MvvmCross.Platforms.Android.Binding.Views;
using MvvmCross.Platforms.Android.Views;

namespace GenesisAuto.Droid.Views
{
    [Activity(Label = "HomeActivity", MainLauncher = true) ]
    public class HomeActivity : BaseActivity<HomeViewModel>
    {
        protected override int ResourceLayoutId => Resource.Layout.drawer_layout;

        private DrawerLayout MyDrawerLayout { get; set; }
        private ActionBarDrawerToggle MyToggle { get; set; }

        private MvxListView ListViewRepositories { get; set; }

        private SwipeRefreshLayout Refresh { get; set; }

        private FloatingActionButton Fab { get; set; }
        private SearchView Search { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Title = Resources.GetString(Resource.String.title_home);

            MyDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            Search = FindViewById<SearchView>(Resource.Id.search);
            Refresh = FindViewById<SwipeRefreshLayout>(Resource.Id.refresh);
            var refreshEmpty = FindViewById<SwipeRefreshLayout>(Resource.Id.refreshEmpty);
            Fab = FindViewById<FloatingActionButton>(Resource.Id.fab);


            MyToggle = new ActionBarDrawerToggle(this, MyDrawerLayout, Resource.String.open_drawer, Resource.String.close_drawer);
            MyDrawerLayout.AddDrawerListener(MyToggle);
            MyToggle.SyncState();

            ListViewRepositories = FindViewById<MvxListView>(Resource.Id.listRep);

            ListViewRepositories.ViewTreeObserver.ScrollChanged += (sender, e) =>
            {
                if (ViewModel.Repositories != null && ViewModel.Repositories.Count > 0 && ListViewRepositories.LastVisiblePosition >= ViewModel.Repositories.Count() - 10)
                {
                    ViewModel.LoadMore(null);
                }
            };

            
            Refresh.Refresh += (sender, e) =>
            {
                ViewModel.RefreshSearch.Execute();
                Refresh.Refreshing = false;
            };

            
            refreshEmpty.Refresh += (sender, e) =>
            {
                ViewModel.RefreshSearch.Execute();
                refreshEmpty.Refreshing = false;
            };
            
            Fab.Click += (sender, e) =>
            {
                ViewModel.SearchVisible = true;
                ShowKeyboard(Search);
            };

            ListViewRepositories.ScrollStateChanged += (sender, e) =>
            {
                if (e.ScrollState == Android.Widget.ScrollState.Idle)
                {
                    Fab.Show();
                }
                else
                {
                    Fab.Hide();
                    ViewModel.SearchVisible = false;
                }
            };

            Search.SetIconifiedByDefault(false);
            Search.QueryTextChange += (object sender, SearchView.QueryTextChangeEventArgs e) =>
            {
                if (string.IsNullOrEmpty(Search.Query) && ViewModel.ShowEmptyState)
                {
                    DoSearch();
                }
            };
            Search.QueryTextSubmit += (object sender, SearchView.QueryTextSubmitEventArgs e) =>
            {
                DoSearch();
            };
        }

        private void DoSearch()
        {
            ViewModel.Search = Search.Query;
            ViewModel.RefreshSearch.Execute();
            ViewModel.SearchVisible = false;
            HideKeyboard(Search);
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