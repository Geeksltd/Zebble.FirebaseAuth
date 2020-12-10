namespace Zebble
{
    using System;

    public class FirebaseUser
    {
        public string IdToken { get; set; }
        public string Email { get; set; }
        public string RefreshToken { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
        public string UserId { get; set; }
    }
}
