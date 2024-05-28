
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
        public DbSet<StateTransition> StateTransition { get; set; }
        public DbSet<Category> Category { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TopicsStates>().HasData(
                new { stateId = 1, ArabicName = "قيد اعداد", EnglishName = "Current" },
                new { stateId = 2, ArabicName = "اعتماد", EnglishName = "Approved" },
                new { stateId = 3, ArabicName = "تصحيح لغوي", EnglishName = "Check Lang" },
                new { stateId = 4, ArabicName = "اعتماد تصميم", EnglishName = "Approve Desgin" },
                new { stateId = 5, ArabicName = "نشر", EnglishName = "Publish" }
                );

            modelBuilder.Entity<StateTransition>().HasData(
                new { StateTransitionId = 1, Statefrom = 1, Stateto = 2 },
                new { StateTransitionId = 2, Statefrom = 2, Stateto = 3 },
                 new { StateTransitionId = 3, Statefrom = 3, Stateto = 4 },
                 new { StateTransitionId = 4, Statefrom = 4, Stateto = 5 },
                new { StateTransitionId = 5, Statefrom = 4, Stateto = 1 },
                 new { StateTransitionId = 6, Statefrom = 3, Stateto = 1 },
                 new { StateTransitionId = 7, Statefrom = 2, Stateto = 1 }
                );

            modelBuilder.Entity<Category>().HasData(
                  new { CategoryId = 1, ArabicName = "توجيه", EnglishName = "Guide" },
                  new { CategoryId = 2, ArabicName = "صور", EnglishName = "Images" },
                  new { CategoryId = 3, ArabicName = "مرئيات", EnglishName = "Videos" },
                  new { CategoryId = 4, ArabicName = "صوتيات", EnglishName = "Audio" }
                );

            base.OnModelCreating(modelBuilder);




        }
    }
}
