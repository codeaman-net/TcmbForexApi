using Microsoft.Extensions.Options;
using TcmbForexApi.ReadAPI.Core.Entities;
using TcmbForexApi.ReadAPI.Core.Repositories;

namespace TcmbForexApi.ReadAPI.Data.MongoDB
{
    public class ForexRateRepository(IOptions<MongoDbSettings> mongoDbSettings) : BaseRepository<Rate>(mongoDbSettings), IForexRateRepository
    {
        
    }
}