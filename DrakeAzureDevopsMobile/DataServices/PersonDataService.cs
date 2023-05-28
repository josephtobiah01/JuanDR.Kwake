using DrakeAzureDevopsMobile.DataServices.Interfaces;
using DrakeAzureDevopsMobile.Models.IntegrationModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeAzureDevopsMobile.DataServices
{
    public class PersonDataService : IPersonDataService
    {
        public async Task<MicrosoftGraphResponse> PostGetProfileAsync(string AccessToken)
        {

            var client = new HttpClient();

            var message = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/me");

            message.Headers.Add("Authorization", $"Bearer {AccessToken}");

            var response = await client.SendAsync(message);

            var responseString = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<MicrosoftGraphResponse>(responseString);

            return data;
        }
    }
}
