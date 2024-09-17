using Domain.Models;

namespace Application.Contracts
{
    public interface ISubscribersRepository
    {
        Task<(List<Subscriber> Subscribers, int TotalCount)> GetSubscribersAsync(
            int page = 1,
            int pageSize = 10,
            string sortColumn = "id",
            string sortDirection = "asc"
        );
        Task<List<Subscriber>> GetSubscribersByEmailsAsync(string[] emails);
        Task<List<Subscriber>> SaveSubscribersAsync(List<Subscriber> subscribers);
        Task<IEnumerable<Subscriber>> GetNewlyExpiredSubscribersAsync();
        Task<Subscriber> GetByIdAsync(string id);
        Task DeleteSubscriberAsync(Subscriber subscriber);
    }
}
