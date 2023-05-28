using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeAzureDevopsMobile.Models.IntegrationModels
{
    public class BaseResponse
    {
        public int statusCode { get; set; }
        public List<string> message { get; set; }
    }
}
