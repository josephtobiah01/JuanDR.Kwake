using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeAzureDevopsMobile.Models.BusinessModels
{
    public class PersonDetails : BaseResponseDetails
    {
        public string Id { get; set; }
        public List<string> BusinessPhones { get; set; } 
        public string DisplayName { get; set; } 
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string JobTitle { get; set; }
        public string Mail { get; set; }
        public string MobilePhone { get; set; }
        public string OfficeLocation { get; set; }
        public string PreferredLanguage { get; set; }


    }
}
