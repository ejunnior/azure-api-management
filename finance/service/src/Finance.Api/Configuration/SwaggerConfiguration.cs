namespace Finance.Api.Configuration
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.AspNetCore.Mvc.Versioning;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    public static class SwaggerConfiguration
    {
        public static IServiceCollection RegisterSwagger(this IServiceCollection services)
        {
            services.AddApiVersioning(setupAction =>
            {
                setupAction.AssumeDefaultVersionWhenUnspecified = true;

                setupAction.DefaultApiVersion = new ApiVersion(
                    majorVersion: 1, minorVersion: 0);

                setupAction.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(setupAction =>
            {
                setupAction.GroupNameFormat = "'v'VV";
            });

            var apiVersionDescriptionProvider =
                services.BuildServiceProvider()
                    .GetService<IApiVersionDescriptionProvider>();

            services.AddSwaggerGen(setupAction =>
            {
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    setupAction
                        .SwaggerDoc(
                            name: $"FinanceAPISpecification{description.GroupName}",
                            info: new OpenApiInfo
                            {
                                Title = "Treasury API",
                                Version = description.ApiVersion.ToString(),
                                Description = "Through this API you can access account payables and receivables information",
                                Contact = new OpenApiContact()
                                {
                                    Email = "ejunnior@gmail.com",
                                    Name = "Edvaldo Junior"
                                },
                                License = new OpenApiLicense()
                                {
                                    Name = "MIT License",
                                    Url = new Uri("https://opensource.org/licenses/MIT")
                                }
                            });
                }

                setupAction.DocInclusionPredicate((documentName, apiDescription) =>
                {
                    var actionApiVersionModel = apiDescription.ActionDescriptor
                        .GetApiVersionModel((ApiVersionMapping.Explicit | ApiVersionMapping.Implicit));

                    if (actionApiVersionModel == null)
                    {
                        return true;
                    }

                    if (actionApiVersionModel.DeclaredApiVersions.Any())
                    {
                        return actionApiVersionModel.DeclaredApiVersions.Any((v =>
                            $"FinanceAPISpecificationv{v.ToString()}" == documentName));
                    }

                    return actionApiVersionModel.ImplementedApiVersions.Any((v =>
                        $"FinanceAPISpecificationv{v.ToString()}" == documentName));
                });

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                setupAction.IncludeXmlComments(xmlCommentsFullPath);
            });

            return services;
        }
    }
}