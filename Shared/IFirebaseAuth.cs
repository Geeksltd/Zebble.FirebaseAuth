namespace Zebble
{
    using System.Threading.Tasks;

    public interface IFirebaseAuth
    {
        void Initialize(string apiKey);
        Task<FirebaseRegisterResult> Register(string email, string password);
        Task<bool> RefreshTokenExpiry();
        Task<bool> IsAuthenticated();
        Task<bool> IsAnonymous();
        Task<FirebaseLoginResult> Login(string email, string password);
        Task<FirebaseUser> GetUser();
        Task Logout();
    }
}
