using GenesisAuto.Core.Models;
using MvvmCross.Commands;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GenesisAuto.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel<object, object>
    {
        private int Page { get; set; } = 1;
        private Task RepositoriesTask { get; set; }
        private int RepositoriesListLimit { get; set; } = 999;

        public override Task Initialize()
        {
            LoadMore(1);
            return base.Initialize();
        }

        private async Task GetRepositories()
        {
            try
            {
                Loading = true;

                string q = "language:JavaScript";

                if (!string.IsNullOrEmpty(Search))
                {
                    q = $"{Search}+in:name+{q}";
                }
                var response = await Apis.GitHub.GetRepositories(q, "Repositories", "stars", Page);

                var rep = response.Content;

                if (rep != null)
                {
                    rep.Items.ForEach((item) =>
                    {
                        if (Repositories.Count <= RepositoriesListLimit)
                        {
                            Repositories.Add(item);
                        }
                    });
                    await RaisePropertyChanged(() => Repositories);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Loading = false;

            ShowEmptyState = Repositories.Count == 0;
        }


        public void LoadMore(int? page)
        {
            try
            {
                if (Repositories.Count <= RepositoriesListLimit && (RepositoriesTask == null || RepositoriesTask.IsCompleted))
                {
                    Page = page.HasValue ? page.Value : Page + 1;
                    RepositoriesTask = GetRepositories();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private string _search;
        public string Search
        {
            get => _search;
            set
            {
                _search = value;
                RaisePropertyChanged(() => Search);
            }
        }

        private bool _searchVisible;
        public bool SearchVisible
        {
            get => _searchVisible;
            set
            {
                _searchVisible = value;
                RaisePropertyChanged(() => SearchVisible);
            }
        }

        private ObservableCollection<Repository> _repositories = new ObservableCollection<Repository>();
        public ObservableCollection<Repository> Repositories
        {
            get => _repositories;
            set
            {
                _repositories = value;
                RaisePropertyChanged(() => Repositories);
            }
        }

        public virtual IMvxCommand RefreshSearch
        {
            get
            {
                return new MvxCommand(() =>
               {
                   Page = 1;
                   Repositories.Clear();
                   LoadMore(1);
               });
            }
        }

        public virtual IMvxCommand SelectRepository
        {
            get
            {
                return new MvxCommand<Repository>(async (rep) =>
                {
                    await NavigationService.Navigate<PullRequestsViewModel, Repository>(rep);
                });
            }
        }


    }
}
