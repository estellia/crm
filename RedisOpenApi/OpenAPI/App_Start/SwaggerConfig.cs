using System.Web.Http;
using WebActivatorEx;
using OpenAPI;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace OpenAPI
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration 
                .EnableSwagger(c =>
                    {                        
                        c.SingleApiVersion("v1", "OpenAPI");                        
                    })
                .EnableSwaggerUi(c =>
                    {                        
                    });
        }

        private static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\bin\OpenApi.XML", System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}