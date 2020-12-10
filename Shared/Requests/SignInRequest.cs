namespace Zebble
{
    class SignInRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool ReturnSecureToken { get; set; } = true;

        public static SignInRequest Create(string email, string password) => new()
        {
            Email = email,
            Password = password
        };
    }
}
