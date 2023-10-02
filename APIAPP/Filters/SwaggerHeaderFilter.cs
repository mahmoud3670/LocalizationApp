using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace APIAPP.Filters
{
    public class SwaggerHeaderFilter : IOperationFilter

    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "language",
                In = ParameterLocation.Header,
                Required = false,
                
            });

        }
    }
}
