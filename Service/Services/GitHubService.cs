using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using Octokit;
using Service.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly GitHubClient _client;
        private readonly GitHubIntegrationOptions _options;
        public GitHubService(IOptions<GitHubIntegrationOptions> option)
        {
            _client = new GitHubClient(new ProductHeaderValue("my-github-app"));
            _options = option.Value;
            _client.Credentials = new Credentials(_options.Token);
        }

        public async Task<List<Portfolio>> GetPortfolio()
        {
            List<Portfolio> result = new List<Portfolio>();

            var repositories = (await _client.Repository.GetAllForUser(_options.UserName)).ToList();

            foreach (Repository repo in repositories)
            {
                var name = repo.Name;
                var languages = repo.Language;
                var latestCommit = repo.UpdatedAt.LocalDateTime;
                var starsNumber = repo.StargazersCount;
                var pullRequestNumber = (await _client.Repository.PullRequest.GetAllForRepository(repo.Id)).Count();
                var link = repo.HtmlUrl;
                result.Add(new Portfolio(name, languages, latestCommit, starsNumber, pullRequestNumber, link));
            }

            return result;
        }

        public async Task<List<Repository>> SearchRepositories(string? repositoryName = "", string? language = "", string? user = "")
        {
            var search = "is:public ";

            if (!string.IsNullOrEmpty(repositoryName))
                search += $"name:{repositoryName} ";

            if (!string.IsNullOrEmpty(language))
                search += $"language:{language} ";
          

            //if (!string.IsNullOrEmpty(repositoryName))
            //    search += $"repo:{repositoryName} ";

            //if (!string.IsNullOrEmpty(language))
            //    search += $"language:{language} ";

            if (!string.IsNullOrEmpty(user))
                search += $"user:{user} ";

            var request = new SearchRepositoriesRequest(search);
            var result = await _client.Search.SearchRepo(request);
            return result.Items.ToList();


            

              

        }
    }
}