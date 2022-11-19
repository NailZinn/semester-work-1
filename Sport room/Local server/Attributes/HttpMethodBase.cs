namespace Local_server.Attributes
{
    internal class HttpMethodBase : Attribute
    {
        public string UriPattern { get; }

        protected HttpMethodBase(string uriPattern)
        {
            UriPattern = uriPattern;
        }
    }
}
