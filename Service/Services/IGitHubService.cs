using Octokit;
using Service.DataEntities;

namespace Service.Services
{
    public interface IGitHubService
    {
        public Task<List<Portfolio>> GetPortfolio();
        public Task<List<Repository>> SearchRepositories(string? repositoryName, string? language, string? userName);
    }
}
