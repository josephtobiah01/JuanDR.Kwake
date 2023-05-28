using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeAzureDevopsMobile.Models.BusinessModels
{
    public class BaseResponseDetails
    {
        public int StatusCode { get; set; }
        public List<string> Message { get; set; }
    }
}
