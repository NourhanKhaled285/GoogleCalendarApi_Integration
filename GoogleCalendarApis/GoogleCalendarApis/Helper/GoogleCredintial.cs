using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;

namespace GoogleCalendarApis.Helper
{
    public static class GoogleCredintial
    {
        public static CalendarService CreateCredintial()
        {

        string[] scopes = { "https://www.googleapis.com/auth/calendar" };
        string applicationName = "Google Calendar API";
        UserCredential credential;
            using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "GoogleAuth", "ClientSecretCredintial.json"), FileMode.Open, FileAccess.Read))
            {
                string credentialPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).
                   Secrets,
                    scopes,
                    "user",
                     CancellationToken.None,
                     new FileDataStore(credentialPath, true)).Result;

            }

            var services = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName,


            });

            return services;
        }
    }
}
