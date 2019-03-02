using ArcTouch.Core.Models;
using ArcTouch.Core.Models.Api;
using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ArcTouch.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel<object, object>
    {
        private int Page { get; set; } = 1;
        private Task MoviesTask { get; set; }

        private List<Genre> Genres { get; set; } = new List<Genre>();
        
        public async override Task Initialize()
        {
            await base.Initialize();

            await GetGenres();

            LoadMore(1);

           
        }

        
        public async Task GetGenres()
        {
            try
            {
                var response = await Apis.IMDBApi.GetGenres(Apis.IMDBApiKey);
                if (response != null && Genres.Count == 0)
                {
                    Genres.AddRange(response.Genres);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
           
        }

        public async Task GetMovies()
        {
            try
            {
                Loading = true;

                Refit.ApiResponse<ResponseIMDBMoviesList> response;
                if (string.IsNullOrEmpty(Search))
                {
                    response = await Apis.IMDBApi.GetUpcomingMovies(Apis.IMDBApiKey, Page);
                }
                else
                {
                    response = await Apis.IMDBApi.SearchMovies(Apis.IMDBApiKey, Page, Search);
                }
                
                var mov = response.Content;

                if (mov != null)
                {
                    mov.Results.ForEach((item) =>
                    {
                       item.Genres = String.Join(", ", Genres.Where(x => item.GenreIds.Contains(x.Id)).Select(x => x.Name));
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

        private ObservableCollection<MovieItem> _movies = new ObservableCollection<MovieItem>();
        public ObservableCollection<MovieItem> Movies
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
                return new MvxCommand(async () =>
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
                return new MvxCommand<MovieItem>(async (mov) =>
                {
                    await NavigationService.Navigate<MovieViewModel, MovieItem>(mov);
                });
            }
        }


    }
}
