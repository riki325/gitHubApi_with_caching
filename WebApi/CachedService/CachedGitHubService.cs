using Microsoft.Extensions.Caching.Memory;
using Octokit;
using Service.DataEntities;
using Service.Services;

namespace GitHubAPI.ChachedService
{
    public class CachedGitHubService : IGitHubService
    {
        private readonly IGitHubService _gitHubService;
        private readonly IMemoryCache _memoryCache;

        private const string UserPortfolioKey = "UserPortfolioKey";
        public CachedGitHubService(IGitHubService gitHubService, IMemoryCache memoryCache)
        {
            _gitHubService = gitHubService;
            _memoryCache = memoryCache;
        }
        public async Task<List<Portfolio>> GetPortfolio()
        {
            if (_memoryCache.TryGetValue(UserPortfolioKey, out List<Portfolio> portfolio))
                return portfolio;

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(30))
                .SetSlidingExpiration(TimeSpan.FromSeconds(10));

             portfolio = (await _gitHubService.GetPortfolio()).ToList();
            _memoryCache.Set(UserPortfolioKey, portfolio, cacheOptions);

            return portfolio;
        }

        public Task<List<Repository>> SearchRepositories(string? language, string? repositoryName, string? userName)
        {
            return _gitHubService.SearchRepositories(language, repositoryName, userName);
        }
    }
}
