using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using Application.Contracts;
using CsvHelper;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscribersController : ControllerBase
    {
        private readonly ISubscribersService _subscribersService;

        public SubscribersController(ISubscribersService subscribersService)
        {
            _subscribersService = subscribersService;
        }

        [HttpGet]
        public async Task<ApiListResponse<List<Subscriber>>> Get(
            int page = 1,
            int pageSize = 10,
            string sortColumn = "id",
            string sortDirection = "asc"
        )
        {
            return await _subscribersService.GetSubscribers(
                page,
                pageSize,
                sortColumn,
                sortDirection
            );
        }

        [HttpPost("import")]
        public async Task<ApiResponse<bool>> ImportSubscribers(IFormFileCollection files)
        {
            var subsToImport = new List<CsvSubscriber>();

            foreach (var file in files)
            {
                using var reader = new StreamReader(file.OpenReadStream());
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                if (
                    file == null
                    || file.Length == 0
                    || Path.GetExtension(file.FileName).ToLower() != ".csv"
                )
                {
                    return new ApiResponse<bool> { Errors = ["Invalid file uploaded."] };
                }

                subsToImport.AddRange(csv.GetRecords<CsvSubscriber>().ToList());
            }

            return await _subscribersService.ImportSubscribers(subsToImport);
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse<bool>> DeleteSubscriber(string id)
        {
            return await _subscribersService.DeleteSubscriber(id);
        }
    }
}
