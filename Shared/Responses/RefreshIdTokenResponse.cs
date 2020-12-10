namespace Zebble
{
    class RefreshIdTokenResponse : ResponseBase
    {
        public string ExpiresIn { get; set; }
        public string TokenType { get; set; }
        public string RefreshToken { get; set; }
        public string IdToken { get; set; }
        public string UserId { get; set; }
        public string ProjectId { get; set; }
    }
}
