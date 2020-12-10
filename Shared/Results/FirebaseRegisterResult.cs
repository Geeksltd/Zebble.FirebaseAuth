namespace Zebble
{
    public class FirebaseRegisterResult
    {
        public bool Succeeded { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }

        public FirebaseUser User { get; set; }

        internal static FirebaseRegisterResult Success(SignUpResponse response) => new()
        {
            Succeeded = true,
            User = new FirebaseUser
            {
                IdToken = response.IdToken,
                Email = response.Email,
                RefreshToken = response.RefreshToken,
                ExpiresAt = response.ExpiresIn.ToUnixOffset(),
                UserId = response.LocalId
            }
        };

        internal static FirebaseRegisterResult Failure(Error error) => new()
        {
            Succeeded = false,
            Code = error.Code,
            Message = error.Message
        };
    }
}
