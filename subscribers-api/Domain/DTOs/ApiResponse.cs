namespace Domain.DTOs
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
    }
}
