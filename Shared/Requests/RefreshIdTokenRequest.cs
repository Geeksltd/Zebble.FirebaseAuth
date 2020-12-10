namespace Zebble
{
    class RefreshIdTokenRequest
    {
        public string RefreshToken { get; set; }

        public static RefreshIdTokenRequest Create(FirebaseUser user) => new()
        {
            RefreshToken = user.RefreshToken
        };
    }
}
