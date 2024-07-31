using Amazon.Runtime;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using tg_client.Models.rest.dtos;

namespace tg_client.Models.rest
{
    public class TGEngineApi : ITGEngineApi
    {

        #region vars
        string url;
        ServiceCollection serviceCollection;
        IHttpClientFactory httpClientFactory;
        HttpClient httpClient;
        #endregion

        public TGEngineApi(string url)
        {
            this.url = url;
            serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var services = serviceCollection.BuildServiceProvider();
            httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
            httpClient = httpClientFactory.CreateClient();
        }

        #region private
        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddHttpClient();
        }
        #endregion

        #region public
        public async Task SendMessage(messageDto message)
        {
            var json = JsonConvert.SerializeObject(message);
            var addr = $"{url}/updates/sendMessage";

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync(addr, data);
                response.EnsureSuccessStatusCode();

            }
            catch (Exception ex)
            {
                throw new Exception($"SetVerificationCode: {ex.Message}");
            }
        }
        #endregion
    }
}
