
using APIAPP.Filters;
using LocalizationLibrary;
using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Reflection;

namespace APIAPP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(swagger =>
            {
                 swagger.OperationFilter<SwaggerHeaderFilter>();
            });





            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //  Resources
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");



            builder.Services.AddMvc().AddViewLocalization().AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    var assemblyName = typeof(CommonResources).GetTypeInfo().Assembly.FullName;
                    if (string.IsNullOrEmpty(assemblyName))
                        throw new ArgumentNullException("Can't find AssemblyName");
                    var assemblyType = new AssemblyName(assemblyName);
                    if (assemblyName == null||string.IsNullOrEmpty(assemblyType.Name))
                        throw new Exception("Can't find Assembly");
                    return factory.Create(nameof(CommonResources), assemblyType.Name);
                };
            });

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ar-EG")
                };
                options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });


            builder.Services.AddTransient<LocalizationFilter>();






            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRequestLocalization();

            app.MapControllers();

            app.Run();
        }
    }
}