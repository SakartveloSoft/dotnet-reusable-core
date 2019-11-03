namespace SakartveloSoft.API.Metadata
{
    public class ValidationError
    {
        public string Property { get; set; }

        public string ErrorCode { get; set; }

        public string Message { get; set; }

        public object Value { get; set; }
    }
}