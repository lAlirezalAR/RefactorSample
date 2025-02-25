namespace Utilities.Framework.Guards
{
    public static class Guard
    {
        public static void AgainstNullValue(object parameter, string message = null)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter), message);
        }

        public static void AgainstNullOrEmpty(string value, string message = null)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value), message);
        }

        public static void AgainstNullOrWhiteSpace(string value, string message = null)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), message);
        }

        public static void AgainstZero(IComparable value, string parameterName, string message = null)
        {
            if (value.CompareTo(0) == 0)
                throw new ArgumentException(message, parameterName);
        }

        public static void AgainstNavigateOrZero(IComparable value, string parameterName, string message = null)
        {
            if (!(value.CompareTo(0) <= 0))
                throw new ArgumentException(message, parameterName);
        }
    }
}
