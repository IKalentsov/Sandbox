using Sandbox.App.Contracts;
using Sandbox.Domain.Entities;

namespace Sandbox.App.Interfaces.Services;

/// <summary>
/// Сервис для работы с пользователями
/// </summary>
public interface IUserService
{
    #region Get

    /// <summary>
    /// Вернуть всех пользователей в системе
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает всех активных пользователей</returns>
    Task<IEnumerable<UserEntity>> GetUsersAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Вернуть пользователя по email
    /// </summary>
    /// <param name="email">Email пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает пользователя по Email</returns>
    Task<UserEntity> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Вернуть пользователя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает пользователя по идентификатору</returns>
    Task<UserEntity> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);

    #endregion

    #region Adding

    /// <summary>
    /// Добавить пользователя в систему
    /// </summary>
    /// <param name="user">Запрос на регистрацию</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает идентификатор созданного пользователя</returns>
    Task<Guid> Register(RegisterUserRequest user, CancellationToken cancellationToken = default);

    #endregion

    #region Updates

    /// <summary>
    /// Обновить пользователя в системе
    /// </summary>
    /// <param name="user">Обновленная сущность пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает обновленную информацию о пользователе</returns>
    Task<UserEntity> UpdateAsync(UserEntity user, CancellationToken cancellationToken = default);

    #endregion

    #region Deleting

    /// <summary>
    /// Удалить пользователя из системы
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает идентификатор удаленного пользователя</returns>
    Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    #endregion
}
