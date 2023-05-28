using DrakeAzureDevopsMobile.Models.IntegrationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeAzureDevopsMobile.DataServices.Interfaces
{
    public interface IPersonDataService
    {
        Task<MicrosoftGraphResponse> PostGetProfileAsync(string AccessToken);
    }
}
