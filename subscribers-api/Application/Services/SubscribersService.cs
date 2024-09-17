using Application.Contracts;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Application.Services
{
    public class SubscribersService : ISubscribersService
    {
        private readonly ISubscribersRepository _subscribersRepository;

        public SubscribersService(ISubscribersRepository subscribersRepository)
        {
            _subscribersRepository = subscribersRepository;
        }

        public async Task<ApiListResponse<List<Subscriber>>> GetSubscribers(
            int page = 1,
            int pageSize = 10,
            string sortColumn = "id",
            string sortDirection = "asc"
        )
        {
            var (subs, totalCount) = await _subscribersRepository.GetSubscribersAsync(
                page,
                pageSize,
                sortColumn,
                sortDirection
            );
            return new ApiListResponse<List<Subscriber>> { Data = subs, TotalItems = totalCount };
        }

        public async Task<ApiResponse<bool>> ImportSubscribers(List<CsvSubscriber> csvSubscribers)
        {
            var errors = await ValidateSubscribers(csvSubscribers);

            if (errors.Any())
            {
                return new ApiResponse<bool> { Data = false, Errors = errors };
            }

            var subscribers = csvSubscribers
                .Select(sub => new Subscriber
                {
                    Email = sub.Email,
                    SubscriptionDate = sub.SubscriptionDate,
                })
                .ToList();

            await _subscribersRepository.SaveSubscribersAsync(subscribers);

            return new ApiResponse<bool> { Data = true };
        }

        public async Task<ApiResponse<bool>> DeleteSubscriber(string id)
        {
            var sub = await _subscribersRepository.GetByIdAsync(id);

            if (sub == null)
            {
                return new ApiResponse<bool> { Data = false, Errors = ["Subscriber not found."] };
            }

            await _subscribersRepository.DeleteSubscriberAsync(sub);

            return new ApiResponse<bool> { Data = true };
        }

        private async Task<List<string>> ValidateSubscribers(List<CsvSubscriber> csvSubscribers)
        {
            List<string> errors = [];

            var now = DateTime.Now;
            var expiredSubs = csvSubscribers.Where(sub => sub.SubscriptionDate <= now).ToList();

            expiredSubs.ForEach(sub =>
            {
                errors.Add($"Subscriber is already expired: {sub.Email}");
            });

            var existingSubs = await _subscribersRepository.GetSubscribersByEmailsAsync(
                csvSubscribers.Select(sub => sub.Email).ToArray()
            );

            existingSubs.ForEach(sub =>
            {
                errors.Add($"Subscriber already exists: {sub.Email}");
            });

            return errors;
        }
    }
}
