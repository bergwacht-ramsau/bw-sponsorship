using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace BW_SPONSORSHIP.Models
{
    public class SponsorContext : DbContext
    {
        public SponsorContext(DbContextOptions<SponsorContext> options)
            : base(options)
        {
        }

        public DbSet<Sponsor> Sponsors { get; set; } = null!;
    }
}