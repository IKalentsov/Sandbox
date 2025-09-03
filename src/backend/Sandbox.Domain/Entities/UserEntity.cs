using Sandbox.Domain.Common;
using System.Text.RegularExpressions;
using Sandbox.Domain.Constants;

namespace Sandbox.Domain.Entities;

public class UserEntity : Auditable, IAggregateRoot
{
    public string Login { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string PasswordHash { get; }
    public int Right { get; }

    public string ProfileImage { get; }

    private UserEntity(Guid id, DateTime created, DateTime modified,
        string login, string firstName, string lastName,
        string email, string passwordHash, int right, string profileImage)
        : base(id, created, modified)
    {
        Login = login;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        Right = right;
        ProfileImage = profileImage;
    }

    public static UserEntity Create(Guid id, DateTime created, DateTime modified, string login, string firstName, string lastName, string email, string passwordHash, int right, string profileImage)
    {
        // Валидация Id и дат
        if (id == Guid.Empty)
            throw new ArgumentException("Id не может быть пустым", nameof(id));
        if(created > modified)
            throw new ArgumentException("Дата создания не может быть больше даты изменения", nameof(created));
        if(modified < created)
            throw new ArgumentException("Дата изменения не может быть меньше даты создания", nameof(modified));

        // Валидация логина
        if (string.IsNullOrWhiteSpace(login))
            throw new ArgumentException("Логин не может быть пустым", nameof(login));
        switch (login.Length)
        {
            case < EntityConstraints.User.LoginMinLength:
                throw new ArgumentException($"Логин должен содержать минимум {EntityConstraints.User.LoginMinLength} символа", nameof(login));
            case > EntityConstraints.User.LoginMaxLength:
                throw new ArgumentException($"Логин может содержать максимум {EntityConstraints.User.LoginMaxLength} символа", nameof(login));
        }

        if (!Regex.IsMatch(login, @"^[a-zA-Z0-9_]+$"))
            throw new ArgumentException("Логин может содержать только буквы, цифры и нижнее подчеркивание", nameof(login));

        // Валидация имени и фамилии
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("Имя не может быть пустым", nameof(firstName));
        switch (firstName.Length)
        {
            case < EntityConstraints.User.FirstNameMinLength:
                throw new ArgumentException($"Имя должно содержать минимум {EntityConstraints.User.FirstNameMinLength} символ", nameof(firstName));
            case > EntityConstraints.User.FirstNameMaxLength:
                throw new ArgumentException($"Имя может содержать максимум {EntityConstraints.User.FirstNameMaxLength} символа", nameof(firstName));
        }

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Фамилия не может быть пустой", nameof(lastName));
        switch (lastName.Length)
        {
            case < EntityConstraints.User.LastNameMinLength:
                throw new ArgumentException($"Фамилия должна содержать минимум {EntityConstraints.User.LastNameMinLength} символ", nameof(lastName));
            case > EntityConstraints.User.LastNameMaxLength:
                throw new ArgumentException($"Фамилия может содержать максимум {EntityConstraints.User.LastNameMaxLength} символа", nameof(lastName));
        }

        // Валидация email
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email не может быть пустым", nameof(email));
        if(email.Length > EntityConstraints.User.EmailMaxLength)
            throw new ArgumentException($"Email может содержать максимум {EntityConstraints.User.EmailMaxLength} символов", nameof(email));
        if (!IsValidEmail(email))
            throw new ArgumentException("Некорректный формат email", nameof(email));

        // Валидация пароля
        if (string.IsNullOrWhiteSpace(passwordHash) || passwordHash.Length < EntityConstraints.User.PasswordMinLength)
            throw new ArgumentException($"Хэш пароля должен содержать минимум {EntityConstraints.User.PasswordMinLength} символов", nameof(passwordHash));

        // Опциональная валидация изображения профиля
        if (!string.IsNullOrEmpty(profileImage) && !Uri.TryCreate(profileImage, UriKind.Absolute, out _))
            throw new ArgumentException("Некорректный формат ссылки на изображение", nameof(profileImage));
        if(profileImage.Length > EntityConstraints.User.ProfileImageMaxLength)
            throw new ArgumentException($"Ссылка на изображение может содержать максимум {EntityConstraints.User.ProfileImageMaxLength} символов", nameof(profileImage));

        return new UserEntity(
            id,
            created,
            modified,
            login.Trim(),
            firstName.Trim(),
            lastName.Trim(),
            email.ToLower().Trim(),
            passwordHash,
            right,
            profileImage.Trim());
    }

    public static UserBuilder Builder() => new UserBuilder();

    public class UserBuilder
    {
        private Guid _id = Guid.NewGuid();
        private DateTime _created = DateTime.UtcNow;
        private DateTime _modified = DateTime.UtcNow;
        private string _login;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _passwordHash;
        private int _right;
        private string _profileImage;

        public UserBuilder()
        {
            // Установка значений по умолчанию
            _right = 0;
            _profileImage = string.Empty;
        }

        public UserBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public UserBuilder WithCreated(DateTime created)
        {
            _created = created;
            return this;
        }

        public UserBuilder WithModified(DateTime modified)
        {
            _modified = modified;
            return this;
        }

        public UserBuilder WithLogin(string login)
        {
            _login = login;
            return this;
        }

        public UserBuilder WithFirstName(string firstName)
        {
            _firstName = firstName;
            return this;
        }

        public UserBuilder WithLastName(string lastName)
        {
            _lastName = lastName;
            return this;
        }

        public UserBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public UserBuilder WithPasswordHash(string passwordHash)
        {
            _passwordHash = passwordHash;
            return this;
        }

        public UserBuilder WithRight(int right)
        {
            _right = right;
            return this;
        }

        public UserBuilder WithProfileImage(string profileImage)
        {
            _profileImage = profileImage;
            return this;
        }

        public UserEntity Build()
        {
            // Валидация Id и дат
            if (_id == Guid.Empty)
                throw new ArgumentException("Id не может быть пустым", nameof(_id));
            if (_created > _modified)
                throw new ArgumentException("Дата создания не может быть больше даты изменения", nameof(_created));
            if (_modified < _created)
                throw new ArgumentException("Дата изменения не может быть меньше даты создания", nameof(_modified));

            // Валидация логина
            if (string.IsNullOrWhiteSpace(_login))
                throw new ArgumentException("Логин не может быть пустым", nameof(_login));
            if (_login.Length < EntityConstraints.User.LoginMinLength)
                throw new ArgumentException($"Логин должен содержать минимум {EntityConstraints.User.LoginMinLength} символа", nameof(_login));
            if (_login.Length > EntityConstraints.User.LoginMaxLength)
                throw new ArgumentException($"Логин может содержать максимум {EntityConstraints.User.LoginMaxLength} символа", nameof(_login));
            if (!Regex.IsMatch(_login, @"^[a-zA-Z0-9_]+$"))
                throw new ArgumentException("Логин может содержать только буквы, цифры и нижнее подчеркивание", nameof(_login));

            // Валидация имени и фамилии
            if (string.IsNullOrWhiteSpace(_firstName))
                throw new ArgumentException("Имя не может быть пустым", nameof(_firstName));
            switch (_firstName.Length)
            {
                case < EntityConstraints.User.FirstNameMinLength:
                    throw new ArgumentException($"Имя должно содержать минимум {EntityConstraints.User.FirstNameMinLength} символ", nameof(_firstName));
                case > EntityConstraints.User.FirstNameMaxLength:
                    throw new ArgumentException($"Имя может содержать максимум {EntityConstraints.User.FirstNameMaxLength} символа", nameof(_firstName));
            }
            if (string.IsNullOrWhiteSpace(_lastName))
                throw new ArgumentException("Фамилия не может быть пустой", nameof(_lastName));
            switch (_lastName.Length)
            {
                case < EntityConstraints.User.LastNameMinLength:
                    throw new ArgumentException($"Фамилия должна содержать минимум {EntityConstraints.User.LastNameMinLength} символ", nameof(_lastName));
                case > EntityConstraints.User.LastNameMaxLength:
                    throw new ArgumentException($"Фамилия может содержать максимум {EntityConstraints.User.LastNameMaxLength} символа", nameof(_lastName));
            }

            // Валидация email
            if (string.IsNullOrWhiteSpace(_email))
                throw new ArgumentException("Email не может быть пустым", nameof(_email));
            if (_email.Length > EntityConstraints.User.EmailMaxLength)
                throw new ArgumentException($"Email может содержать максимум {EntityConstraints.User.EmailMaxLength} символов", nameof(_email));
            if (!IsValidEmail(_email))
                throw new ArgumentException("Некорректный формат email", nameof(_email));

            // Валидация пароля
            if (string.IsNullOrWhiteSpace(_passwordHash) || _passwordHash.Length < EntityConstraints.User.PasswordMinLength)
                throw new ArgumentException($"Хэш пароля должен содержать минимум {EntityConstraints.User.PasswordMinLength} символов", nameof(_passwordHash));
            if (_profileImage.Length > EntityConstraints.User.ProfileImageMaxLength)
                throw new ArgumentException($"Ссылка на изображение может содержать максимум {EntityConstraints.User.ProfileImageMaxLength} символов", nameof(_profileImage));

            return new UserEntity(
                _id,
                _created,
                _modified,
                _login.Trim(),
                _firstName.Trim(),
                _lastName.Trim(),
                _email.ToLower().Trim(),
                _passwordHash,
                _right,
                _profileImage.Trim());
        }
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
