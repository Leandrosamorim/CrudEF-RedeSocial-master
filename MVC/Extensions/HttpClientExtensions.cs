using Domain.Models.Interfaces.Services;
using Domain.Models.Options;
using Domain.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication14.HttpServices;

namespace WebApplication14.Extensions
{
    public static class HttpClientExtensions
    {
        public static void RegisterHttpClients(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var professorHttpOptionsSection = configuration.GetSection(nameof(ProfessorHttpOptions));
            var professorHttpOptions = professorHttpOptionsSection.Get<ProfessorHttpOptions>();


            services.Configure<ProfessorHttpOptions>(configuration.GetSection(nameof(ProfessorHttpOptions)));
            services.AddHttpClient(professorHttpOptions.Name, x => { x.BaseAddress = professorHttpOptions.ApiBaseUrl; });

            services.AddScoped<IProfessorHttpServices, ProfessorHttpServices>();
            services.AddScoped<IPostHttpServices, PostHttpService>();
        }
    }
}
