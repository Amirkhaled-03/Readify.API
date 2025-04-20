namespace Readify.BLL.Constants
{
    public static class AppConstants
    {
        // Maximum number of failed login attempts before lockout
        public const int MaxLoginAttempts = 6;

        // Access failure thresholds
        public static readonly int[] LockedOutThresholds = { 3, 4, 5 };

        public const int DefaultPageSize = 10;
        public const string DateFormat = "yyyy-MM-dd";
    }
}
