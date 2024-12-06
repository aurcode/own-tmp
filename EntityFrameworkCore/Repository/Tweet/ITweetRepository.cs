namespace EntityFrameworkCore.Repository.Tweet
{
    public interface ITweetRepository : IRepository<int, Core.Entities.Tweet.Tweet>
    {
        Task<IEnumerable<Core.Entities.Tweet.Tweet>> GetByUserIdAsync(int id, int number);
    }
}
