using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeAzureDevopsMobile.Models.IntegrationModels
{
    public class GetPersonProfileResponse : BaseResponse
    {
        public GetPersonProfileResponseData data { get; set; }
    }
}
