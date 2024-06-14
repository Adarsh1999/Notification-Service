using Microsoft.EntityFrameworkCore;

namespace NotificationConsumer.Models
{
    public class NotificationDbContext : DbContext
    {
        public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options) 
        {
        }
        public DbSet<NotificationUpdateRequest> NotificationUpdates { get; set; }
    }
}
