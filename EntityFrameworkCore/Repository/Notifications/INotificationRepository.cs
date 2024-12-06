namespace EntityFrameworkCore.Repository.Notification
{
    public interface INotificationRepository : IRepository<int, Core.Entities.Notifications.Notification>
    {
        Task<IEnumerable<Core.Entities.Notifications.Notification>> GetByUserIdAsync(int id, int number);
    }
}
