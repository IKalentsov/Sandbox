using Microsoft.EntityFrameworkCore;
using Sandbox.App.Interfaces.Common;
using Sandbox.App.Interfaces.Repositories;
using Sandbox.DataAccess.Persistence;
using Sandbox.Domain.Entities;

namespace Sandbox.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SandboxDbContext _context;

    public UserRepository(SandboxDbContext context)
    {
        _context = context;
    }

    #region Get

    public async Task<UserEntity> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var userEntity = await _context.Users
                             .AsNoTracking()
                             .FirstOrDefaultAsync(u => u.Email == email, cancellationToken)
                         ?? throw new Exception($"Пользователь с email: {email} не найден.");

        return UserEntity.Create(userEntity.Id,
            userEntity.Created,
            userEntity.Modified,
            userEntity.Login,
            userEntity.FirstName,
            userEntity.LastName,
            userEntity.Email,
            userEntity.PasswordHash,
            userEntity.Right,
            userEntity.ProfileImage);
    }

    public Task<UserEntity> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserEntity>> GetUsersAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Add

    public async Task<Guid> AddAsync(UserEntity user, CancellationToken cancellationToken = default)
    {
        var id = await _context.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return id.Entity.Id;
    }

    #endregion

    #region Update

    public Task<UserEntity> UpdateAsync(UserEntity user, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Delete

    public Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    #endregion
}
