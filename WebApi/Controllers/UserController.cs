using Microsoft.AspNetCore.Mvc;
using Octokit;
using Service.DataEntities;
using Service.Services;

namespace GitHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGitHubService _gitHubService;
        public UserController(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }
        [HttpGet("/github/getPortfolio")]
        public async Task<List<Portfolio>> GetPortfolio()
        {

            var result = await _gitHubService.GetPortfolio();
            return result.ToList();
        }
        [HttpGet("/github/SearchPublicRepositories")]
        public async Task<List<Repository>> SerchRepositories(string? repositoryName, string? language, string? userName)
        {
            var resutl = await _gitHubService.SearchRepositories(repositoryName, language, userName);
            return resutl;
        }
    }
}
