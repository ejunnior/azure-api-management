namespace Finance.Api.Configuration
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.AspNetCore.Routing;

    public static class WebApiApplicationBuilder
    {
        public static IApplicationBuilder Configure(
            this IApplicationBuilder app,
            IWebHostEnvironment hostingEnvironment,
            IApiVersionDescriptionProvider apiVersionDescriptionProvider,
            Action<IEndpointRouteBuilder> endpointConfigurator = null)
        {
            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    setupAction.SwaggerEndpoint(
                        url: $"/swagger/FinanceAPISpecification{description.GroupName}/swagger.json",
                        name: description.GroupName.ToUpperInvariant());
                }

                setupAction.RoutePrefix = string.Empty;
            });

            app.UseMiddleware<ExceptionHandler>();

            app.UseRouting();

            app.UseCors(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            app.UseEndpoints(endPoints =>
            {
                endPoints.MapControllers();
            });

            return app;
        }
    }
}