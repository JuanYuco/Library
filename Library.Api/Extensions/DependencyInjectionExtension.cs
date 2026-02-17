using Library.Application.Interfaces;
using Library.Application.Services;
using Library.Domain.Ports;
using Library.Infrastructure;
using Library.Infrastructure.Adapters;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddDbContext<AppDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));

            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();

            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
            return services;
        }
    }
}
