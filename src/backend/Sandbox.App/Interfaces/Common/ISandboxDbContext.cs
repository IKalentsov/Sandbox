using Microsoft.EntityFrameworkCore;
using Sandbox.Domain.Entities;

namespace Sandbox.App.Interfaces.Common;

public interface ISandboxDbContext
{
    public DbSet<UserEntity> Users { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}
