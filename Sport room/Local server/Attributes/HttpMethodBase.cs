namespace Local_server.Attributes
{
    internal abstract class HttpMethodBase : Attribute
    {
        public string UriPattern { get; }

        protected HttpMethodBase(string uriPattern)
        {
            UriPattern = uriPattern;
        }
    }
}
