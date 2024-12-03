namespace Microsoft.Extensions.DependencyInjection;

public static class DataExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<InMemoryData>();
        services.AddSingleton<AuthorRepository>();
        services.AddSingleton<BookRepository>();
        services.AddSingleton<BookChapterRepository>();
        return services;
    }
}