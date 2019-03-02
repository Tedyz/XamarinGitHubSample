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
        [Get("/search/movie")]
        Task<ApiResponse<ResponseIMDBMoviesList>> SearchMovies(string api_key, int page, string query);

        [Get("/movie/{movieId}")]
        Task<List<PullRequest>> GetMovieDetails([AliasAs("movieId")] string movieId, string api_key);
    }
}
