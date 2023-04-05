namespace TeploAPI.Models
{
    public class Response
    {
        public bool IsSuccess { get; set; } = false;

        public string? ErrorMessage { get; set; }

        public int Status { get; set; }

        public string? SuccessMessage { get; set; }

        public object? Result { get; set; }
    }
}
