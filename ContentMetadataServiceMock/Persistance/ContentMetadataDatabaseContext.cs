using Microsoft.EntityFrameworkCore;
using ContentMetadataServiceMock.Persistance.Data;

namespace ContentMetadataServiceMock.Persistance
{
    
    
    public class ContentMetadataDatabaseContext : DbContext
    {
       
        public DbSet<ContentData> Contents { get; set; }
        public DbSet<LicenseRulesData> LicenseRules { get; set; }
        public DbSet<ContentCommentData> ContentComments { get; set; }

        public ContentMetadataDatabaseContext(DbContextOptions<ContentMetadataDatabaseContext> options)
            : base(options)
        {
        }

        //TODO: It is needed???
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Konfiguracja relacji 1<->n (ContentData <-> ContentCommentsData)
        //    modelBuilder.Entity<ContentData>()
        //        .HasMany(c => c.Comments)
        //        .WithOne(cc => cc.ContentData)
        //        .HasForeignKey(cc => cc.ContentDataId)
        //        .OnDelete(DeleteBehavior.Cascade);

        //    // Konfiguracja relacji 1<->1 (ContentData <-> LicenseRulesData)
        //    modelBuilder.Entity<ContentData>()
        //        .HasOne(c => c.LicenseRules)
        //        .WithOne(lr => lr.ContentData)
        //        .HasForeignKey<ContentData>(c => c.LicenseRulesDataId)
        //        .OnDelete(DeleteBehavior.Cascade);
        //}
    }
}
