namespace Local_server.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class ApiController : Attribute
    {
        public string Uri { get; }

        public ApiController(string uri)
        {
            Uri = uri;
        }
    }
}
