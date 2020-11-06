using Domain.Models.Interfaces.Repositories;
using Domain.Models.Interfaces.Services;
using Domain.Services.Services;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Context.WebApplication14.Data;

namespace Crosscutting
{
    public static class DependencyInjection
    {
        public static void Register(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddDbContext<WebApplication14Context>(options =>
                     options.UseSqlServer(configuration.GetConnectionString("WebApplication14Context")));

            services.AddScoped<IProfessorRepository, ProfessorRepository>();
            services.AddScoped<IProfessorServices, ProfessorServices>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostServices, PostServices>();

            services.AddScoped<IBlobService, BlobServices>(provider =>
            new BlobServices(configuration.GetValue<string>("StorageAccount")));

            services.AddScoped<IQueueMessage, QueueServices>(provider =>
                new QueueServices(configuration.GetValue<string>("StorageAccount")));
        }
    }
}

