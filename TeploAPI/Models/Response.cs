namespace TeploAPI.Models
{
    public class Response
    {
        public bool IsSuccess { get; set; }

        public string? ErrorMessage { get; set; }

        public int StatusCode { get; set; }

        public string? Result { get; set; }
    }
}
