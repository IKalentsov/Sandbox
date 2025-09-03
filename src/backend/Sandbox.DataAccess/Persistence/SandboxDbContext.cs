using Microsoft.EntityFrameworkCore;
using Sandbox.App.Interfaces.Common;
using Sandbox.Domain.Entities;

namespace Sandbox.DataAccess.Persistence;

public class SandboxDbContext : DbContext, ISandboxDbContext
{
    public SandboxDbContext(DbContextOptions<SandboxDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SandboxDbContext).Assembly);
    }
}
