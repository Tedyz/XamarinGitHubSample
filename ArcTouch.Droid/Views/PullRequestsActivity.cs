using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using ArcTouch.Core.ViewModels;
using MvvmCross.Platforms.Android.Binding.Views;

namespace ArcTouch.Droid.Views
{
    [Activity(Label = "Pull Requests")]
    public class PullRequestsActivity : BaseActivity<PullRequestsViewModel>
    {
        protected override int ResourceLayoutId => Resource.Layout.activity_pullrequests;

        private SwipeRefreshLayout SwipeRefresh { get; set; }
        private SwipeRefreshLayout SwipeRefreshEmpty { get; set; }
        private MvxListView ListViewPullRequests { get; set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SwipeRefresh = FindViewById<SwipeRefreshLayout>(Resource.Id.refresh);
            SwipeRefreshEmpty = FindViewById<SwipeRefreshLayout>(Resource.Id.refreshEmpty);
            ListViewPullRequests = FindViewById<MvxListView>(Resource.Id.listRep);

            SwipeRefresh.Refresh += Refresh;
            SwipeRefreshEmpty.Refresh += Refresh;

            ListViewPullRequests.ViewTreeObserver.ScrollChanged += (sender, e) =>
            {
                if (ViewModel.PullRequests != null && ViewModel.PullRequests.Count > 0 && ListViewPullRequests.LastVisiblePosition >= ViewModel.PullRequests.Count() - 10)
                {
                    ViewModel.LoadMore(null);
                }
            };

            Title = ViewModel.Repository.Name;
        }

        private void Refresh(object sender, EventArgs e)
        {
            ViewModel.RefreshSearch.Execute();
            SwipeRefreshEmpty.Refreshing = false;
            SwipeRefresh.Refreshing = false;
        }
    }
}