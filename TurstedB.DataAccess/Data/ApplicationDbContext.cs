
using TrustedB.DataAccess;
using TrustedB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TrustedB.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }

       
        public DbSet<Topics> Topics { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<StateHistory> StateHistory { get; set; }
        public DbSet<CommentHistory> CommentHistory { get; set; }
        public DbSet<Attachments> Attachments { get; set; }
        public DbSet<TopicsStates> TopicsStates { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           

        }
    }
}
