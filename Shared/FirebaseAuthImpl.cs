namespace Zebble
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Zebble.Device;

    public class FirebaseAuthImpl : IFirebaseAuth
    {
        internal static string ApiKey { get; private set; }

        public static void Initialize(string apiKey)
        {
            ApiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
        }

        public async Task<FirebaseRegisterResult> Register(string email, string password)
        {
            var request = SignUpRequest.Create(email, password);

            var response = await HttpUtils.IdentityToolkit<SignUpResponse>("accounts:signUp", request);

            if (response.Error != null)
                return FirebaseRegisterResult.Failure(response.Error);

            var result = FirebaseRegisterResult.Success(response);

            await PersistUser(result.User);

            return result;
        }

        public async Task<bool> RefreshTokenExpiry()
        {
            var user = await GetUser();

            if (user != null)
            {
                if (user.ExpiresAt.IsPast())
                {
                    await Logout();
                    return false;
                }

                var request = RefreshIdTokenRequest.Create(user);

                var response = await HttpUtils.SecureToken<RefreshIdTokenResponse>("token", request);

                if (response.Error != null)
                    return false;

                user.ExpiresAt = response.ExpiresIn.ToUnixOffset();
                user.IdToken = response.IdToken;

                if (user.ExpiresAt.IsPast())
                {
                    await Logout();
                    return false;
                }

                await PersistUser(user);
                return true;
            }

            return false;
        }

        public async Task<bool> IsAuthenticated() => await GetUser() != null;

        public async Task<bool> IsAnonymous() => !await IsAuthenticated();

        public async Task<FirebaseLoginResult> Login(string email, string password)
        {
            var request = SignInRequest.Create(email, password);

            var response = await HttpUtils.IdentityToolkit<SignInResponse>("accounts:signInWithPassword", request);

            if (response.Error != null)
                return FirebaseLoginResult.Failure(response.Error);

            var result = FirebaseLoginResult.Success(response);

            await PersistUser(result.User);

            return result;
        }

        public Task<FirebaseUser> GetUser() => LoadUser();

        public Task Logout() => PersistUser(null);

        Task PersistUser(FirebaseUser result) => GetUserFile().WriteAllTextAsync(result.ToJson());

        async Task<FirebaseUser> LoadUser()
        {
            var userFile = GetUserFile();

            if (userFile.Exists())
                return (await userFile.ReadAllTextAsync()).FromJson<FirebaseUser>();

            return null;
        }

        FileInfo GetUserFile() => IO.File("Zebble_FirebaseAuth_User.json");
    }
}
