namespace Sandbox.Domain.Common.Enums;

/// <summary>
/// Уровни прав пользователя в системе
/// </summary>
public enum UserRight
{
    /// <summary>
    /// Нет специальных прав
    /// </summary>
    None = 0,

    /// <summary>
    /// Администратор системы
    /// </summary>
    Admin = 1,

    /// <summary>
    /// Гость системы
    /// </summary>
    Guest = 2,
}
