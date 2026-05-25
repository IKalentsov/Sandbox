namespace Sandbox.App.Interfaces.Auth;

/// <summary>
/// Интерфейс для хеширования и проверки паролей
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Генерирует хэш из пароля
    /// </summary>
    /// <param name="password">Пароль для хеширования</param>
    /// <returns>Хэш пароля</returns>
    string Generate(string password);

    /// <summary>
    /// Проверяет соответствие пароля хэшу
    /// </summary>
    /// <param name="password">Пароль для проверки</param>
    /// <param name="hashedPassword">Сохраненный хэш</param>
    /// <returns>true если пароль совпадает, иначе false</returns>
    bool Verify(string password, string hashedPassword);
}
