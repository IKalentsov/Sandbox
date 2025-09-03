using Sandbox.Domain.Entities;

namespace Sandbox.App.Interfaces.Repositories;

/// <summary>
/// Репозиторий пользователей
/// </summary>
public interface IUserRepository
{
    #region Get

    /// <summary>
    /// Вернуть всех пользователей в системе
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>Возвращает всех активных пользователей</returns>
    Task<IEnumerable<UserEntity>> GetUsersAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Вернуть пользователя по email
    /// </summary>
    /// <param name="email"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Возвращает пользователя по Email</returns>
    Task<UserEntity> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Вернуть пользователя по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Возвращает пользователя по идентификатору</returns>
    Task<UserEntity> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);

    #endregion

    #region Addind

    /// <summary>
    /// Добавить пользователя в систему
    /// </summary>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Возвращает идентификатор пользователя</returns>
    Task<Guid> AddAsync(UserEntity user, CancellationToken cancellationToken = default);

    #endregion

    #region Updates

    /// <summary>
    /// Обновить пользователя в системе
    /// </summary>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Возвращает информацию о пользователе</returns>
    Task<UserEntity> UpdateAsync(UserEntity user, CancellationToken cancellationToken = default);

    #endregion

    #region Deleting

    /// <summary>
    /// Удалить пользователя из системы
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Возвращает идентификатор пользователя</returns>
    Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    #endregion
}
