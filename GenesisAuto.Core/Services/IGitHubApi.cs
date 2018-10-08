using GenesisAuto.Core.Models.Api;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GenesisAuto.Core.Services
{
    public interface IGitHubApi
    {
        [Get("/search/repositories")]
        Task<ResponseGitHubRepositories> GetRepositories(string q, string sort, int page);

        [Get("/repos/{fullName}/pulls")]
        Task<ResponseGitHubRepositories> GetPullRequests([AliasAs("fullName")] string fullName);
    }

}
