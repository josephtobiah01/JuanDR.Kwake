using DrakeAzureDevopsMobile.Models.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeAzureDevopsMobile.BusinessControllers.Interfaces
{
    public interface IPersonController
    {
        Task<PersonDetails> GetPersonProfileAsync(string AccessToken);

    }
}
