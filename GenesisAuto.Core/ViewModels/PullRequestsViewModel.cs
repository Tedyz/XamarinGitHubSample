using GenesisAuto.Core.Models;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace GenesisAuto.Core.ViewModels
{
    public class PullRequestsViewModel : BaseViewModel<Repository,object>
    {
        private Repository Repository { get; set; }
        private int Page { get; set; } = 1;
        private Task PullRequestTask { get; set; }

        public override void Prepare(Repository parameter)
        {
            Repository = parameter;

            LoadMore(1);
        }

        private async Task GetPullRequests()
        {
            try
            {
                Loading = true;

              
                var response = await Apis.GitHub.GetPullRequests(Repository.FullName, Page);

                var rep = response;

                if (rep != null)
                {
                    rep.ForEach((item) => { PullRequests.Add(item); });
                    await RaisePropertyChanged(() => PullRequests);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Loading = false;

            ShowEmptyState = PullRequests.Count == 0;
        }

        private ObservableCollection<PullRequest> _pullRequests = new ObservableCollection<PullRequest>();
        public ObservableCollection<PullRequest> PullRequests
        {
            get => _pullRequests;
            set
            {
                _pullRequests = value;
                RaisePropertyChanged(() => PullRequests);
            }
        }


        public void LoadMore(int? page)
        {
            try
            {
                if (PullRequestTask == null || PullRequestTask.IsCompleted)
                {
                    Page = page.HasValue ? page.Value : Page + 1;
                    PullRequestTask = GetPullRequests();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public virtual IMvxCommand RefreshSearch
        {
            get
            {
                return new MvxCommand(() =>
                {
                    Page = 1;
                    PullRequests.Clear();
                    LoadMore(1);
                });
            }
        }
    }
}
