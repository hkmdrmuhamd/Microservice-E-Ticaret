using Microsoft.EntityFrameworkCore;
using MultiShop.Comment.Entities;

namespace MultiShop.Comment.Context
{
    public class CommentContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=localhost;Port=3305;Database=MultiShopCommentDb;User=*****;Password=*****;";
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(5, 7, 0)));
        }

        public DbSet<UserComment> UserComments { get; set; }
    }
}
