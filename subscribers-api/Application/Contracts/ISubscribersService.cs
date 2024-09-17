using Domain.DTOs;
using Domain.Models;

namespace Application.Contracts
{
    public interface ISubscribersService
    {
        Task<ApiListResponse<List<Subscriber>>> GetSubscribers(
            int page = 1,
            int pageSize = 10,
            string sortColumn = "id",
            string sortDirection = "asc"
        );
        Task<ApiResponse<bool>> ImportSubscribers(List<CsvSubscriber> csvSubscribers);
        Task<ApiResponse<bool>> DeleteSubscriber(string id);
    }
}
