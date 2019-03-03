using ArcTouch.Core.Models;
using ArcTouch.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArcTouch.Core.ViewModels
{
    public class MovieViewModel : BaseViewModel<MovieItem, object>
    {

        public MovieItem Movie { get; set; }
        private MoviesDetails _movieDetails;
        public MoviesDetails MovieDetails
        {
            get => _movieDetails;
            set
            {
                SetProperty(ref _movieDetails, value);
            }
        }


        public override void Prepare(MovieItem parameter)
        {
            Movie = parameter;
        }

        public async override Task Initialize()
        {
            await GetMovieDetail();
        }

        private async Task GetMovieDetail()
        {
            Loading = true;
            try
            {
                if (Movie != null)
                {
                    MovieDetails = await Apis.IMDBApi.GetMovieDetails(Movie.Id, Apis.IMDBApiKey);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }            
           
            Loading = false;
        }
    }
}
