using BlogPost.Areas.Identity.Data;
using BlogPost.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Data
{
    public class ApplicationDbContext : IdentityDbContext<BlogPostUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Post> posts { get; set; }
    }
}