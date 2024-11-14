using GithubTestApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GithubTestApp.Models
{
    public class TodoContext : DbContext
    {
        public IConfiguration _configuration { get; set; }
        public TodoContext(DbContextOptions<TodoContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public TodoContext()
        {
            
        }

        public DbSet<ToDo> ToDos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
            }
        }
    }
}
