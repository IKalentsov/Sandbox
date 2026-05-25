namespace Sandbox.Domain.Common;

/// <summary>
/// Базовый класс для сущностей с аудитами
/// </summary>
public class Auditable
{
    /// <summary>
    /// Создает экземпляр Auditable
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="created">Дата создания</param>
    /// <param name="modified">Дата последнего изменения</param>
    public Auditable(Guid id, DateTime created, DateTime modified)
    {
        Id = id;
        Created = created;
        Modified = modified;
    }

    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Дата создания записи
    /// </summary>
    public DateTime Created { get; }

    /// <summary>
    /// Дата последнего изменения записи
    /// </summary>
    public DateTime Modified { get; }
}
