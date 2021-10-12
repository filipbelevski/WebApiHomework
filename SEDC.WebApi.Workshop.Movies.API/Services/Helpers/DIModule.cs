using DataAccess;
using DataAccess.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Helpers
{
    public static class DIModule
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IMovieRepository, MovieRepository>();
            //services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IUserRepository, DapperUserRepository>(x => new DapperUserRepository(connectionString));

            services.AddDbContext<MoviesDbContext>(x => x.UseSqlServer(connectionString));

            return services;
        }
    }
}
