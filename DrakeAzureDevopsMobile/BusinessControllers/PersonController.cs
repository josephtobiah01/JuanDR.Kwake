using DrakeAzureDevopsMobile.BusinessControllers.Interfaces;
using DrakeAzureDevopsMobile.DataServices.Interfaces;
using DrakeAzureDevopsMobile.Models.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeAzureDevopsMobile.BusinessControllers
{
    public class PersonController : IPersonController
    {

        private readonly IPersonDataService _personDataService;
        private readonly ISessionController _sessionController;

        public PersonController(
            IPersonDataService personDataService,
            ISessionController sessionController)
        {
            _personDataService = personDataService ?? throw new ArgumentNullException(nameof(personDataService));
            _sessionController = sessionController ?? throw new ArgumentNullException(nameof(sessionController));
        }

        public async Task<PersonDetails> GetPersonProfileAsync(string AccessToken)
        {

            var result = await _personDataService.PostGetProfileAsync(_sessionController.AccessToken);

            if (result is null)
                return new PersonDetails() { StatusCode = result.statusCode, Message = result.message };

            var data = new PersonDetails()
            {
                StatusCode = result.statusCode,
                Message = result.message,
                BusinessPhones = result.businessPhones,
                DisplayName = result.displayName,
                GivenName = result.givenName,
                Surname = result.surname,
                Mail = result.mail,
                MobilePhone = result.mobilePhone,
                JobTitle = result.jobTitle,
                OfficeLocation = result.officeLocation,
                PreferredLanguage = result.preferredLanguage
            };
            return data;
        }
    }
}
