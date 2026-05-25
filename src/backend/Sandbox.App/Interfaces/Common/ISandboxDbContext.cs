using Microsoft.EntityFrameworkCore;
using Sandbox.Domain.Entities;

namespace Sandbox.App.Interfaces.Common;

/// <summary>
/// Интерфейс контекста базы данных Sandbox
/// </summary>
public interface ISandboxDbContext
{
    /// <summary>
    /// Коллекция пользователей
    /// </summary>
    DbSet<UserEntity> Users { get; set; }

    /// <summary>
    /// Сохраняет все изменения в базе данных
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Количество сохраненных элементов</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}
