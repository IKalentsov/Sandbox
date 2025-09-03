using Sandbox.App.Contracts;
using Sandbox.App.Interfaces.Auth;
using Sandbox.App.Interfaces.Repositories;
using Sandbox.App.Interfaces.Services;
using Sandbox.Domain.Common.Enums;
using Sandbox.Domain.Entities;

namespace Sandbox.App.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Register(RegisterUserRequest user, CancellationToken cancellationToken = default)
    {
        var hashedPassword = _passwordHasher.Generate(user.Password);

        var userEntity = UserEntity.Create(
            Guid.NewGuid(),
            DateTime.UtcNow,
            DateTime.UtcNow,
            user.Login,
            user.FirstName,
            user.LastName,
            user.Email,
            hashedPassword,
            (int)UserRight.None,
            user.ProfileImage);

        return await _userRepository.AddAsync(userEntity, cancellationToken);
    }

    public async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<UserEntity> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _userRepository.GetUserByEmailAsync(email, cancellationToken);
    }

    public async Task<UserEntity> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UserEntity>> GetUsersAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<UserEntity> UpdateAsync(UserEntity user, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
