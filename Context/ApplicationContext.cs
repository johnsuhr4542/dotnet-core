using System;
using application.Models;
using Microsoft.EntityFrameworkCore;

namespace application.Context {
  public class ApplicationContext: DbContext {
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}

    public DbSet<Member> Member { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.Entity<Member>(entity => {
        entity.HasKey(e => e.Username);
        entity.Property(e => e.Username).HasColumnName("Username").HasColumnType("nvarchar(64)");
        entity.Property(e => e.Password).HasColumnName("Password").HasColumnType("nvarchar(128)");
      });
    }
  }
}