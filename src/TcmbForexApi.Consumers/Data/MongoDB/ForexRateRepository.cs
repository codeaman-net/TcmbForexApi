using Microsoft.Extensions.Options;
using TcmbForexApi.Consumers.Core.Entities;
using TcmbForexApi.Consumers.Core.Repositories;

namespace TcmbForexApi.Consumers.Data.MongoDB
{
    public class ForexRateRepository(IOptions<MongoDbSettings> mongoDbSettings) : BaseRepository<Rate>(mongoDbSettings), IForexRateRepository
    {
        
    }
}