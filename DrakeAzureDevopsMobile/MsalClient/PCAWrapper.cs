using Microsoft.Identity.Client;
using Microsoft.Maui.Animations;
using Microsoft.Maui.Controls;

namespace DrakeAzureDevopsMobile.MsalClient
{
    /// <summary>
    /// This is a wrapper for PCA. It is singleton and can be utilized by both application and the MAM callback
    /// </summary>
    public class PCAWrapper
    {
        public static PCAWrapper Instance { get; private set; } = new PCAWrapper();

        internal IPublicClientApplication PCA { get; }

        internal bool UseEmbedded { get; set; } = false;

        internal const string Authority = "https://login.microsoftonline.com/ddee6582-e98e-4c7d-9f92-523e8c9df537";
        internal const string ClientId = "e90e60d1-95bb-4f32-878c-845ef6286067";
        internal const string TenantId = "ddee6582-e98e-4c7d-9f92-523e8c9df537";
        public static string[] Scopes = { /*"api://e90e60d1-95bb-4f32-878c-845ef6286067/access_as_user"*/ "user.read" };

        // private constructor for singleton
        private PCAWrapper()
        {

#if ANDROID
            // Create PCA once. Make sure that all the config parameters below are passed
            PCA = PublicClientApplicationBuilder
                                        .Create(ClientId)
                                        .WithRedirectUri(PlatformConfig.Instance.RedirectUri)
                                        .WithIosKeychainSecurityGroup("com.drakeintl.onedrakeapp")
                                        .WithBroker(true)
                                        .Build();
#else
            PCA = PublicClientApplicationBuilder
                                        .Create(ClientId)
                                        .WithRedirectUri("onedrakeapp://auth")
                                        .WithIosKeychainSecurityGroup("com.drakeintl.onedrakeapp")
                                        .Build();

#endif
        }

        /// <summary>
        /// Acquire the token silently
        /// </summary>
        /// <param name="scopes">desired scopes</param>
        /// <returns>Authentication result</returns>
        public async Task<AuthenticationResult> AcquireTokenSilentAsync(string[] scopes)
        {
            var accts = await PCA.GetAccountsAsync();
            var acct = accts.FirstOrDefault();

            var authResult = await PCA.AcquireTokenSilent(scopes, acct)
                                        .ExecuteAsync(CancellationToken.None); 

            return authResult;

        }

        /// <summary>
        /// Perform the interactive acquisition of the token for the given scope
        /// </summary>
        /// <param name="scopes">desired scopes</param>
        /// <returns></returns>
        public async Task<AuthenticationResult> AcquireTokenInteractiveAsync(string[] scopes)
        {
            SystemWebViewOptions systemWebViewOptions = new SystemWebViewOptions();
#if IOS
            // embedded view is not supported on Android
            if (UseEmbedded)
            {

                return await PCA.AcquireTokenInteractive(scopes)
                                        .WithPrompt(Prompt.ForceLogin)
                                        .WithUseEmbeddedWebView(true)
                                        .WithParentActivityOrWindow(PlatformConfig.Instance.ParentWindow)
                                        .ExecuteAsync();
            }

            // Hide the privacy prompt in iOS
            systemWebViewOptions.iOSHidePrivacyPrompt = true;
#endif

            return await PCA.AcquireTokenInteractive(scopes)
                                    .WithPrompt(Prompt.ForceLogin)
                                    .WithAuthority(Authority)
                                    .WithParentActivityOrWindow(PlatformConfig.Instance.ParentWindow)
                                    .WithUseEmbeddedWebView(true)
                                    .ExecuteAsync();
        }

        /// <summary>
        /// Signout may not perform the complete signout as company portal may hold
        /// the token.
        /// </summary>
        /// <returns></returns>
        public async Task SignOutAsync()
        {
            var accounts = (await PCA.GetAccountsAsync()).ToList();


            while (accounts.Any())
            {
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    
                    await PCA.RemoveAsync(accounts.First());
                    accounts = (await PCA.GetAccountsAsync()).ToList();
                });
                
            }
        }
    }
}
