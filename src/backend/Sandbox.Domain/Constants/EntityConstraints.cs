namespace Sandbox.Domain.Constants;

public static class EntityConstraints
{
    public static class User
    {
        public const int LoginMinLength = 3;
        public const int LoginMaxLength = 50;
        public const int FirstNameMinLength = 1;
        public const int FirstNameMaxLength = 150;
        public const int LastNameMinLength = 1;
        public const int LastNameMaxLength = 150;
        public const int EmailMaxLength = 320;
        public const int PasswordHashMaxLength = 500;
        public const int ProfileImageMaxLength = 500;
        public const int PasswordMinLength = 8;
    }
}
