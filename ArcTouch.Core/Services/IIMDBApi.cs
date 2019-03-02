using ArcTouch.Core.Models;
using ArcTouch.Core.Models.Api;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArcTouch.Core.Services
{
    [Headers("User-Agent: :request:")]
    public interface IIMDBApi
    {
        [Get("/movie/upcoming")]
        Task<ApiResponse<ResponseIMDBMoviesList>> GetUpcomingMovies(string api_key, int page);
        
        [Get("/search/movie")]
        Task<ApiResponse<ResponseIMDBMoviesList>> SearchMovies(string api_key, int page, string query);

        [Get("/movie/{movieId}")]
        Task<MoviesDetails> GetMovieDetails([AliasAs("movieId")] int movieId, string api_key);

        [Get("/genre/movie/list")]
        Task<MoviesDetails> GetGenres(string api_key);
        
    }
}
