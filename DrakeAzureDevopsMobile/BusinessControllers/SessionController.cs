using DrakeAzureDevopsMobile.BusinessControllers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeAzureDevopsMobile.BusinessControllers
{
    public class SessionController : ISessionController
    {
        public string AccessToken { get; set; }
    }
}
