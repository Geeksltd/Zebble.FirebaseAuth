namespace Zebble
{
    using System.Collections.Generic;

    abstract class ResponseBase
    {
        public Error Error { get; set; }
    }

    class Error
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public IReadOnlyCollection<ErrorItem> Errors { get; set; }
    }

    class ErrorItem
    {
        public string Domain { get; set; }
        public string Reason { get; set; }
        public string Message { get; set; }
    }
}
