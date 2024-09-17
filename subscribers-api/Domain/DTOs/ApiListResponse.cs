namespace Domain.DTOs
{
    public class ApiListResponse<T> : ApiResponse<T>
    {
        public int TotalItems { get; set; }
    }
}
