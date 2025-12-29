namespace TaskTracker.Blazor.Domain.Constants;

public static class AppConstants
{
    public const string AuthTokenKey = "authToken";
    public const string UserKey = "currentUser";

    public static class Roles
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }

    public static class TaskPriorities
    {
        public const string Low = "Low";
        public const string Medium = "Medium";
        public const string High = "High";
        public const string Critical = "Critical";
    }
}
