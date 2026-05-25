namespace Sandbox.Domain.Constants;

/// <summary>
/// Констанры ограничений для доменных сущностей
/// </summary>
public static class EntityConstraints
{
    /// <summary>
    /// Константы ограничений для сущности User
    /// </summary>
    public static class User
    {
        /// <summary>
        /// Минимальная длина логина
        /// </summary>
        public const int LoginMinLength = 3;

        /// <summary>
        /// Максимальная длина логина
        /// </summary>
        public const int LoginMaxLength = 50;

        /// <summary>
        /// Минимальная длина имени
        /// </summary>
        public const int FirstNameMinLength = 1;

        /// <summary>
        /// Максимальная длина имени
        /// </summary>
        public const int FirstNameMaxLength = 150;

        /// <summary>
        /// Минимальная длина фамилии
        /// </summary>
        public const int LastNameMinLength = 1;

        /// <summary>
        /// Максимальная длина фамилии
        /// </summary>
        public const int LastNameMaxLength = 150;

        /// <summary>
        /// Максимальная длина email
        /// </summary>
        public const int EmailMaxLength = 320;

        /// <summary>
        /// Максимальная длина хэша пароля
        /// </summary>
        public const int PasswordHashMaxLength = 500;

        /// <summary>
        /// Максимальная длина ссылки на изображение профиля
        /// </summary>
        public const int ProfileImageMaxLength = 500;

        /// <summary>
        /// Минимальная длина пароля
        /// </summary>
        public const int PasswordMinLength = 8;
    }
}
