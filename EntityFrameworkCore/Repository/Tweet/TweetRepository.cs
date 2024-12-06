using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Repository.Tweet
{
    public class TweetRepository : Repository<int, Core.Entities.Tweet.Tweet>, ITweetRepository
    {
        public TweetRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Core.Entities.Tweet.Tweet>> GetByUserIdAsync(int id, int number)
        {
            var x = Context.Tweets.Include(x => x.UserId).Where(x => x.UserId == id).OrderBy(x => x.Created).Take(number);
            return x;
        }
    }
}
