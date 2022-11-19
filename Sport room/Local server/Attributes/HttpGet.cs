namespace Local_server.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    internal class HttpGet : HttpMethodBase
    {
        public HttpGet(string uriPattern): base(uriPattern) { }
    }
}
