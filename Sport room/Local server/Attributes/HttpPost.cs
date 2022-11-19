namespace Local_server.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    internal class HttpPost : HttpMethodBase
    {
        public HttpPost(string uriPattern): base(uriPattern) { }
    }
}
