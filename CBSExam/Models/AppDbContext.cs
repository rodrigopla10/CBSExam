using Microsoft.EntityFrameworkCore;

namespace CBSExam.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Message> Message { get; set; }

        public DbSet<SentMessage> SentMessage { get; set; }
    }
}
