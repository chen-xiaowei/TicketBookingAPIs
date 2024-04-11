using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TicketBookingAPIs.Filters
{
    public class DefaultValueSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if(schema == null)
            {
                return;
            }
            foreach(var p in schema.Properties)
            {
                if(p.Value.Type == "string" && p.Value.Default == null)
                {
                    p.Value.Default = new OpenApiString("");
                }
                //TokenRequest
                if (p.Key == "userName")
                {
                    p.Value.Default = new OpenApiString("admin");
                }
                else if (p.Key == "password")
                {
                    p.Value.Default = new OpenApiString("123456");
                }

            }
        }
    }
}
