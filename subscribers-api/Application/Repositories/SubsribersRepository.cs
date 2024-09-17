using Application.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repositories
{
    public class SubscribersRepository : ISubscribersRepository
    {
        private readonly DataContext _dataContext;

        public SubscribersRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<(List<Subscriber> Subscribers, int TotalCount)> GetSubscribersAsync(
            int page = 1,
            int pageSize = 10,
            string sortColumn = "id",
            string sortDirection = "asc"
        )
        {
            var totalCount = await _dataContext.Subscribers.CountAsync();

            var query = _dataContext.Subscribers.AsQueryable();
            query = ApplySorting(query, sortColumn, sortDirection);

            var subscribers = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return (subscribers, totalCount);
        }

        public async Task<List<Subscriber>> GetSubscribersByEmailsAsync(string[] emails)
        {
            return await _dataContext
                .Subscribers.Where(s => emails.Contains(s.Email))
                .ToListAsync();
        }

        public async Task<List<Subscriber>> SaveSubscribersAsync(List<Subscriber> subscribers)
        {
            _dataContext.AddRange(subscribers);
            await _dataContext.SaveChangesAsync();
            return subscribers;
        }

        public async Task<IEnumerable<Subscriber>> GetNewlyExpiredSubscribersAsync()
        {
            var now = DateTime.Now;
            var tenMinutesAgo = now.AddMinutes(-10);

            return await _dataContext
                .Subscribers.Where(s =>
                    s.SubscriptionDate <= now && s.SubscriptionDate >= tenMinutesAgo
                )
                .ToListAsync();
        }

        public async Task<Subscriber> GetByIdAsync(string id) 
        {
            return await _dataContext.Subscribers.FindAsync(id);
        }

        public async Task DeleteSubscriberAsync(Subscriber subscriber)
        {
           _dataContext.Subscribers.Remove(subscriber);
           await _dataContext.SaveChangesAsync();
        }

        private IQueryable<Subscriber> ApplySorting(
            IQueryable<Subscriber> query,
            string sortColumn,
            string sortDirection
        )
        {
            switch (sortColumn.ToLower())
            {
                case "email":
                    return sortDirection == "asc"
                        ? query.OrderBy(s => s.Email)
                        : query.OrderByDescending(s => s.Email);
                case "subscriptiondate":
                    return sortDirection == "asc"
                        ? query.OrderBy(s => s.SubscriptionDate)
                        : query.OrderByDescending(s => s.SubscriptionDate);
                default:
                    return sortDirection == "asc"
                        ? query.OrderBy(s => s.Id)
                        : query.OrderByDescending(s => s.Id);
            }
        }
    }
}
