namespace Local_server
{
    internal static class StringConstants
    {
        public const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SportRoomDB;Integrated Security=True;";
        public const string PostsTemplatePath = "Views/posts.sbnhtml";
        public const string PostTemplatePath = "Views/postTemplate.sbnhtml";
        public const string EventsTemplatePath = "Views/events.sbnhtml";
        public const string LoginTemplatePath = "Views/login.sbnhtml";
        public const string RegistrationTemplatePath = "Views/registration.sbnhtml";
        public const string LoginRawUrl = "/login";
        public const string MainRawUrl = "/";
        public const string CommentsTemplatePath = "Views/comments.sbnhtml";
        public const string AccountSettingsPath = "Views/account.sbnhtml";
        public const string ErrorPagePath = "./static/html/errorPage.html";
    }
}
