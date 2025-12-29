namespace TaskTracker.Blazor.Domain.Constants;

public static class ApiRoutes
{
    public const string BaseUrl = "https://localhost:7163/api";

    public static class Auth
    {
        public const string Login = "/auth/login";
        public const string Register = "/auth/register";
    }

    public static class Boards
    {
        public const string GetAll = "/boards";
        public const string GetById = "/boards/{id}";
        public const string GetMy = "/boards/my";
        public const string Create = "/boards";
        public const string Update = "/boards/{id}";
        public const string Delete = "/boards/{id}";
        public const string Archive = "/boards/{id}/archive";
    }

    public static class Tasks
    {
        public const string GetById = "/tasks/{id}";
        public const string GetByList = "/tasks/list/{listId}";
        public const string GetMy = "/tasks/my";
        public const string Create = "/tasks";
        public const string Update = "/tasks/{id}";
        public const string Delete = "/tasks/{id}";
    }

    public static class Comments
    {
        public const string GetByTask = "/comments/task/{taskId}";
        public const string Create = "/comments";
        public const string Update = "/comments/{id}";
        public const string Delete = "/comments/{id}";
    }
}
