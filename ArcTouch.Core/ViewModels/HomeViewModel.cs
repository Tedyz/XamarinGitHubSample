using ArcTouch.Core.Models;
using MvvmCross.Commands;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ArcTouch.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel<object, object>
    {
        private int Page { get; set; } = 1;
        private Task MoviesTask { get; set; }

        public override Task Initialize()
        {
            LoadMore(1);
            return base.Initialize();
        }

        private async Task GetMovies()
        {
            try
            {
                Loading = true;

                if(Search == null)
                    Search = "Ring";
                
                var response = await Apis.IMDBApi.SearchMovies(Apis.IMDBApiKey, Page, Search);

                var rep = response.Content;

                if (rep != null)
                {
                    rep.Results.ForEach((item) =>
                    {
                       Movies.Add(item);
                    });
                    await RaisePropertyChanged(() => Movies);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Loading = false;

            ShowEmptyState = Movies.Count == 0;
        }


        public void LoadMore(int? page)
        {
            try
            {
                if (MoviesTask == null || MoviesTask.IsCompleted)
                {
                    Page = page.HasValue ? page.Value : Page + 1;
                    MoviesTask = GetMovies();
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

        private ObservableCollection<MoviesResponse> _movies = new ObservableCollection<MoviesResponse>();
        public ObservableCollection<MoviesResponse> Movies
        {
            get => _movies;
            set
            {
                _movies = value;
                RaisePropertyChanged(() => Movies);
            }
        }

        public virtual IMvxCommand RefreshSearch
        {
            get
            {
                return new MvxCommand(() =>
               {
                   Page = 1;
                   Movies.Clear();
                   LoadMore(1);
               });
            }
        }

        public virtual IMvxCommand SelectMovie
        {
            get
            {
                return new MvxCommand<MoviesResponse>(async (rep) =>
                {
                    //await NavigationService.Navigate<PullRequestsViewModel, Repository>(rep);
                });
            }
        }


    }
}
