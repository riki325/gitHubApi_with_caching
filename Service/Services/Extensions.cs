﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public static class Extensions
    {
        public static void AddGitHubIntegration(this IServiceCollection services, Action<GitHubIntegrationOptions> configureOptions)
        {
            services.Configure(configureOptions);
            //services.AddScoped<IGitHubService, GitHubService>();
        }
    }
}
