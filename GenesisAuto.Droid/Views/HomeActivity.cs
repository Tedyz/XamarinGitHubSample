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
using Android.Support.V4.View;

namespace GenesisAuto.Droid.Views
{
    [Activity(Label = "GitHub Awesome", Theme = "@style/AppTheme") ]
    public class HomeActivity : BaseActivity<HomeViewModel>
    {
        protected override int ResourceLayoutId => Resource.Layout.drawer_layout;

        private DrawerLayout MyDrawerLayout { get; set; }
        private ActionBarDrawerToggle MyToggle { get; set; }

        private MvxListView ListViewRepositories { get; set; }

        private SwipeRefreshLayout SwipeRefresh { get; set; }
        private SwipeRefreshLayout SwipeRefreshEmpty { get; set; }

        private FloatingActionButton Fab { get; set; }
        private SearchView Search { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Title = Resources.GetString(Resource.String.title_home);

            MyDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            Search = FindViewById<SearchView>(Resource.Id.search);
            SwipeRefresh = FindViewById<SwipeRefreshLayout>(Resource.Id.refresh);
            SwipeRefreshEmpty = FindViewById<SwipeRefreshLayout>(Resource.Id.refreshEmpty);
            Fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            ListViewRepositories = FindViewById<MvxListView>(Resource.Id.listRep);

            MyToggle = new ActionBarDrawerToggle(this, MyDrawerLayout, Resource.String.open_drawer, Resource.String.close_drawer);
            MyDrawerLayout.AddDrawerListener(MyToggle);
            MyToggle.SyncState();

            

            ListViewRepositories.ViewTreeObserver.ScrollChanged += (sender, e) =>
            {
                if (ViewModel.Repositories != null && ViewModel.Repositories.Count > 0 && ListViewRepositories.LastVisiblePosition >= ViewModel.Repositories.Count() - 10)
                {
                    ViewModel.LoadMore(null);
                }
            };
        
            SwipeRefresh.Refresh += Refresh;
            SwipeRefreshEmpty.Refresh += Refresh;
            
            Fab.Click += (sender, e) =>
            {
                if(ViewModel.SearchVisible)
                {
                    DoSearch();
                }
                else
                {
                    ViewModel.SearchVisible = true;
                    ShowKeyboard(Search);
                }
                
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
                if(string.IsNullOrEmpty(Search.Query))
                {
                    DoSearch(false);
                }
            };
            Search.QueryTextSubmit += (object sender, SearchView.QueryTextSubmitEventArgs e) =>
            {
                DoSearch();
            };
        }
        
        private void Refresh(object sender, EventArgs e)
        {
            ViewModel.RefreshSearch.Execute();
            SwipeRefreshEmpty.Refreshing = false;
            SwipeRefresh.Refreshing = false;
        }

        private void DoSearch(bool hideSearch = true)
        {
            ViewModel.Search = Search.Query;
            ViewModel.RefreshSearch.Execute();

            if(hideSearch)
            {
                ViewModel.SearchVisible = false;
                HideKeyboard(Search);
            }
        }

        public override void OnBackPressed()
        {
            if(MyDrawerLayout.IsDrawerOpen(GravityCompat.Start))
            {
                MyDrawerLayout.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
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