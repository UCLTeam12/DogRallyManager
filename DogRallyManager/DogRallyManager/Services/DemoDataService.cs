using DogRallyManager.DbContexts;
using DogRallyManager.Entities;

namespace DogRallyManager.Services
{
    public class DemoDataService : IDataService
    {
        private readonly DogRallyDbContext _dogRallyDbContext;

        public DemoDataService(DogRallyDbContext dogRallyDbContext)
        {
            _dogRallyDbContext = dogRallyDbContext;
        }

        public async Task<List<Message>> GetAllMessagesAsync()
        {
            return _dogRallyDbContext.Messages.ToList();
        }

        public async Task AddMessageAsync(Message message)
        {
            await _dogRallyDbContext.Messages.AddAsync(message);
            await _dogRallyDbContext.SaveChangesAsync();
        }

    }
}
