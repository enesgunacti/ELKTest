using ELKTest.Models;
using Nest;

namespace ELKTest.Extensions
{
    public static class ElasticSearchExtensions
    {
        public static void AddElasticSearch(this IServiceCollection services,IConfiguration configuration)
        {
            var url = configuration["ELKConfiguration:Uri"];
            var defaultIndex = configuration["ELKConfiguration:index"];

            var settings = new ConnectionSettings(new Uri(url)).PrettyJson().DefaultIndex(defaultIndex);

            AddDefaultMappings(settings);

            var client = new ElasticClient(settings);
            services.AddSingleton<IElasticClient>(client);

            CreateIndex(client, defaultIndex);
        }
        private static void AddDefaultMappings(ConnectionSettings settings)
        {         
            settings.DefaultMappingFor<Sales>(p =>
                p
                );
        }

        private static void CreateIndex(IElasticClient client,string indexName) 
        {
          
            client.Indices.Create(indexName, i => i.Map<Sales>(x => x.AutoMap()));
        }
    }
}
