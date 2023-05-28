using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeAzureDevopsMobile.Interfaces
{
    public interface IAzureLoginPageViewModel
    {
        bool EmailAddSignInIsVisible { get; set; }
        bool BiometricsSignInIsVisible { get; set; }
    }
}
