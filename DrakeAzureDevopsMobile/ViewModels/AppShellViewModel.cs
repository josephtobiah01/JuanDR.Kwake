using DrakeAzureDevopsMobile.Services.Interfaces;
using Plugin.LocalNotification;
using Plugin.LocalNotification.EventArgs;

namespace DrakeAzureDevopsMobile.ViewModels
{
    public class AppShellViewModel
    {

        #region Fields

        
        private string _versionName;
        private string _versionString;
        private string _buildString;


        #endregion Fields

        #region Properties

        public string VersionName
        {
            get => _versionName;
            set => _versionName= value;
        }

        public string VersionString
        {
            get => _versionString;
            set => _versionString= value;
        }

        public string BuildString
        {
            get => _buildString;
            set => _buildString= value;
        }


        #endregion Properties

        #region Constructor
        public AppShellViewModel() 
        {

            Initialize();
        }
        #endregion Constructor

        #region Methods

        private void Initialize()
        {
            VersionName = AppInfo.Name;
            BuildString = AppInfo.BuildString;
            
        }

        


        #endregion Methods

    }
}
