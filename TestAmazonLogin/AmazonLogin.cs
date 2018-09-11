using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;
using System.Web;
using Windows.Security.Authentication.Web;
using Alexa.NET.Management;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RealTalkSkillManagement
{
    public class AmazonLogin
    {
        private const string TokenRequestUrl = "https://www.amazon.com/ap/oa";
        private const string AccessRequestUrl = "https://api.amazon.com/auth/o2/token";


        public AmazonLogin(string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        public string ClientSecret { get; }

        public string ClientId { get; }

        private static readonly JsonSerializer Serializer = JsonSerializer.Create();

        private Dictionary<string, string> GetCodeDetails(string grantType, string codeType, string code)
        {
            var callbackUri = WebAuthenticationBroker.GetCurrentApplicationCallbackUri().ToString().Replace("ms-app://", "https://imacode.ninja/");

            return new Dictionary<string, string>
            {
                {"grant_type", grantType},
                {codeType,code},
                {"client_id",ClientId},
                {"client_secret",ClientSecret},
                {"redirect_uri",callbackUri}
            };
        }

        public async Task<AccessInformation> AuthRequestAsync()
        {
            var callbackUri = WebAuthenticationBroker.GetCurrentApplicationCallbackUri().ToString().Replace("ms-app://", "https://imacode.ninja/");
            var state = Guid.NewGuid();

            var requestUri = $"{TokenRequestUrl}?client_id={ClientId}&scope={AuthorizationScopes.ReadWriteSkills} {AuthorizationScopes.ReadModels} {AuthorizationScopes.TestingSkills} &response_type=code&state={state:N}&redirect_uri={callbackUri}";
            var result = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, new Uri(requestUri, UriKind.Absolute), new Uri(callbackUri, UriKind.Absolute)).;
            var queryParams = HttpUtility.ParseQueryString(new Uri(result.ResponseData).Query);

            if (result.ResponseStatus != WebAuthenticationStatus.Success || state.ToString("N") != queryParams["state"])
            {
                return null;
            }

            var authCode = queryParams["code"];

            var dictionary = GetCodeDetails("authorization_code", "code", authCode);


            var authResponse = await new HttpClient().PostAsync(AccessRequestUrl, new FormUrlEncodedContent(dictionary));
            if (!authResponse.IsSuccessStatusCode)
            {
                return null;
            }

            using (var json = new JsonTextReader(new StreamReader(await authResponse.Content.ReadAsStreamAsync())))
            {
                return Serializer.Deserialize<AccessInformation>(json);
            }
        }

        public static AccessInformation InformationFromString(string json)
        {
            var response = Serializer.Deserialize<AccessInformation>(new JsonTextReader(new StringReader(json)));
            return response;
        }

        private static AccessInformation CurrentToken { get; set; }
        private static DateTime? ExpiresOn { get; set; }

        public async Task<Func<Task<string>>> TokenAuthorizer()
        {
            TryLocalData();
            if (CurrentToken == null)
            {
                CurrentToken = await AuthRequestAsync();
                if (CurrentToken == null)
                {
                    throw new AuthenticationException("Token not generated");
                }

                UpdateToken();
            }

            return async () =>
            {
                if (ExpiresOn > DateTime.UtcNow)
                {
                    return CurrentToken.AccessToken;
                }

                await RefreshToken();

                if (CurrentToken == null)
                {
                    throw new AuthenticationException("Refresh token not generated");
                }

                SaveData();

                return CurrentToken.AccessToken;
            };
        }

        private async Task RefreshToken()
        {
            var dictionary = GetCodeDetails("refresh_token", "refresh_token", CurrentToken.RefreshToken);


            var authResponse = await new HttpClient().PostAsync(AccessRequestUrl, new FormUrlEncodedContent(dictionary));
            if (!authResponse.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Unable to refresh amazon token");
            }

            using (var json = new JsonTextReader(new StreamReader(await authResponse.Content.ReadAsStreamAsync())))
            {
                CurrentToken = Serializer.Deserialize<AccessInformation>(json);
                UpdateToken();
            }
        }

        private static void UpdateToken()
        {
            ExpiresOn = DateTime.UtcNow.AddSeconds(CurrentToken.ExpiresIn);
            SaveData();
        }

        private static void SaveData()
        {
            var settings = Windows.Storage.ApplicationData.Current.LocalSettings;
            settings.Values["tokendata"] = JObject.FromObject(CurrentToken).ToString(Formatting.None);
            settings.Values["tokendate"] = ExpiresOn.Value.ToUniversalTime().ToString("O");
        }

        private static void TryLocalData()
        {
            if (CurrentToken != null)
            {
                return;
            }

            var settings = Windows.Storage.ApplicationData.Current.LocalSettings;
            var rawData = settings.Values["tokendata"] as string;
            CurrentToken = string.IsNullOrWhiteSpace(rawData) ? null : InformationFromString(rawData);

            if (CurrentToken != null)
            {
                ExpiresOn = DateTime.Parse(settings.Values["tokendate"].ToString());
            }
        }
    }
}