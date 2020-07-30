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
        entity.Property(e => e.Username).HasColumnName("username").HasColumnType("nvarchar(64)");
        entity.Property(e => e.Password).HasColumnName("password").HasColumnType("nvarchar(128)");
        entity.Property(e => e.RegDate).HasColumnName("reg_date").HasColumnType("datetime").HasDefaultValue(DateTime.Now);
      });
    }
  }
}