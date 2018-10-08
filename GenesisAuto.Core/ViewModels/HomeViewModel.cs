using GenesisAuto.Core.Models;
using GenesisAuto.Core.Models.Api;
using GenesisAuto.Core.Services;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace GenesisAuto.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {

        public override Task Initialize()
        {
            LoadMore(1);
            return base.Initialize();
        }

        private int Page { get; set; } = 1;

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
                var response = await Apis.GitHub.GetRepositories(q,"Repositories", "stars", Page);

                var rep = response.Content;

                if (rep != null)
                {
                    rep.Items.ForEach((item) => { Repositories.Add(item); });
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

        SemaphoreSlim filterSemaphore = new SemaphoreSlim(1, 1);
        private Task RepositoriesTask { get; set; }
        public void LoadMore(int? page)
        {
            try
            {
                if (RepositoriesTask == null || RepositoriesTask.IsCompleted)
                {
                    Page = page.HasValue ? page.Value : Page + 1;
                    RepositoriesTask = GetRepositories();
                }
            }
            catch(Exception e)
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
        
        private ObservableCollection<Repositories> _repositories = new ObservableCollection<Repositories>();
        public ObservableCollection<Repositories> Repositories
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
                return new MvxCommand( () =>
                {
                    Page = 1;
                    Repositories.Clear();
                    LoadMore(1);
                });
            }
        }
    }
}
