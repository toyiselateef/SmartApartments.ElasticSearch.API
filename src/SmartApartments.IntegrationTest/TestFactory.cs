using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartApartments.IntegrationTest
{

   public class TestFactory<T> : WebApplicationFactory<T> where T : class
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var logger = scopedServices.GetRequiredService<ILogger<ApplicationFactory<T>>>();
                };
            });
        }

        public HttpClient GetAnonymousClient()
        {
            return CreateClient();
        }

    }