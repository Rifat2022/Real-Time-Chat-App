using Microsoft.EntityFrameworkCore;

namespace LetsChatApplication.Model
{
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> options):base(options)
        {
            
        }

        public DbSet<UsersInfo> UsersInfo { get; set; }
        public DbSet<MessageInfo> MessageInfo { get; set; }

    }
}
