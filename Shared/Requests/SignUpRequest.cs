namespace Zebble
{
    class SignUpRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool ReturnSecureToken { get; set; } = true;

        public static SignUpRequest Create(string email, string password) => new()
        {
            Email = email,
            Password = password
        };
    }
}
