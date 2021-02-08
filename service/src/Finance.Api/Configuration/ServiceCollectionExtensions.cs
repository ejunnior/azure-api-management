namespace Finance.Api.Configuration
{
    using Domain.Bank.Aggregates.BankAccountAggregate;
    using Domain.Category.Aggregates.CategoryAggregate;
    using Domain.Creditor.Aggregates.CreditorAggregate;
    using Domain.Treasury.Aggregates.BankPostingAggregate;
    using Infrastructure.Data.Bank.Repositories;
    using Infrastructure.Data.Category.Repositories;
    using Infrastructure.Data.Creditor.Repositories;
    using Infrastructure.Data.Treasury.Repositories;
    using Infrastructure.Data.UnitOfWork;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            return services
                .AddRepositories()
                .AddUnitOfWork();
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICreditorRepository, CreditorRepository>();
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            services.AddScoped<IBankPostingRepository, BankPostingRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }

        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddScoped<IFinanceUnitOfWork, FinanceUnitOfWork>();
        }
    }
}