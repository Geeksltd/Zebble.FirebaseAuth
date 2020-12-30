namespace Zebble
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Zebble.Device;
    using Olive;

    static class HttpUtils
    {
        const string IDENTITY_TOOLKIT_ADDRESS = "https://identitytoolkit.googleapis.com/";
        const string SECURE_TOKEN_ADDRESS = "https://securetoken.googleapis.com/";
        const string REQUEST_PATH = "/v1/{0}?key={1}";

        public static Task<T> IdentityToolkit<T>(string path, object request, Encoding encoding = null) where T : ResponseBase, new()
        {
            return Post<T>(IDENTITY_TOOLKIT_ADDRESS, path, request, encoding);
        }

        public static Task<T> SecureToken<T>(string path, object request, Encoding encoding = null) where T : ResponseBase, new()
        {
            return Post<T>(SECURE_TOKEN_ADDRESS, path, request, encoding);
        }

        static async Task<T> Post<T>(string baseAddress, string path, object request, Encoding encoding) where T : ResponseBase, new()
        {
            encoding ??= Encoding.UTF8;

            try
            {
                var client = CreateClient(baseAddress);

                var uri = CreateRequestUri(path);
                var payload = new StringContent(request.ToJson(), encoding, "text/json");

                var message = await client.PostAsync(uri, payload);

                return encoding.GetString(await message.Content.ReadAsByteArrayAsync()).FromJson<T>();
            }
            catch (Exception ex)
            {
                return CreateDefault<T>(ex);
            }
        }

        static T CreateDefault<T>(Exception ex) where T : ResponseBase, new()
        {
            return new T
            {
                Error = new Error
                {
                    Code = -1,
                    Message = $"Can't connect to Firebase Auth APIs. {ex.Message}"
                }
            };
        }

        static HttpClient CreateClient(string baseAddress) => Network.HttpClient(baseAddress, TimeSpan.FromSeconds(30));

        static string CreateRequestUri(string path)
        {
            if (!FirebaseAuthImpl.ApiKey.HasValue())
                throw new Exception($"{nameof(FirebaseAuthImpl.ApiKey)} isn't specified.");

            return REQUEST_PATH.FormatWith(path, FirebaseAuthImpl.ApiKey);
        }
    }
}
