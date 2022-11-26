using Local_server.ActionResults;
using Local_server.Attributes;
using Local_server.Services;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Local_server
{
    internal static class ResponseMaker
    {
        public static async Task MakeAsync(HttpListenerRequest request, HttpListenerResponse response, ServerSettings settings)
        {
            if (Regex.IsMatch(request.RawUrl, @"^/css|html|images|js/.\.css|html|jpg|png|js"))
            {
                ProcessStatic(request, response);
                return;
            }

            var paths = SplitUrlSegments(request.RawUrl);

            #region

            var apiController = (ApiController)Type
                .GetType("Local_server.Attributes.ApiController")!
                .GetConstructor(new[] { typeof(string) })!
                .Invoke(new[] { paths.controller });

            var controllerType = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(type => type.GetCustomAttribute<ApiController>()?.Uri == apiController.Uri);

            if (controllerType is null)
            {
                await LoadErrorPage(response);
                return;
            }

            var method = controllerType
                .GetMethods()
                .FirstOrDefault(method => method
                    .GetCustomAttributes()
                    .Any(attr =>
                    {
                        var type = attr.GetType();
                        var uriPattern = type
                            .GetProperty("UriPattern")?
                            .GetValue(attr)?
                            .ToString() ?? "";

                        return Regex.IsMatch(paths.method, uriPattern) &&
                               type.Name.ToLower() == $"http{request.HttpMethod.ToLower()}";
                    }));

            if (method is null)
            {
                await LoadErrorPage(response);
                return;
            }

            #endregion

            var methodArgs = GetMethodArgs(paths.method, request, method);

            var result = (ActionResult) await (method.Invoke(Activator.CreateInstance(controllerType), methodArgs) as dynamic);

            response.ContentType = result.ContentType;
            response.StatusCode = (int)result.StatusCode;

            if (result.Cookies is not null)
                response.Cookies.Add(result.Cookies);

            if (response.StatusCode == (int)HttpStatusCode.Redirect)
                response.Headers.Set(HttpResponseHeader.Location, @$"http://localhost:{settings.Port}{result.RedirectPath}");
            else
            {
                using var output = response.OutputStream;
                output.Write(result.Buffer, 0, result.Buffer.Length);
            }
        }

        private static void ProcessStatic(HttpListenerRequest request, HttpListenerResponse response)
        {
            var buffer = File.ReadAllBytes("./static/" + request.RawUrl);
            using var output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
        }

        private static (string controller, string method) SplitUrlSegments(string rawUrl)
        {
            if (rawUrl.Length == 1)
                return (rawUrl, "");
            else
            {
                if (Regex.IsMatch(rawUrl, "^/(football|hockey|tennis|basketball|volleyball)$"))
                {
                    return (rawUrl[0].ToString(), rawUrl);
                }
                else
                {
                    var controller = "/" + string.Join("", rawUrl.Skip(1).TakeWhile(c => c != '/'));
                    var method = rawUrl.Replace(controller, "");

                    return (controller, method);
                }
            }
        }

        private static object[] GetMethodArgs(string uri, HttpListenerRequest request, MethodInfo method)
        {
            var parameters = method.GetParameters();
            var args = uri.Split('/').Skip(1).ToList();

            var query = new List<string>();

            if (request.HasEntityBody)
            {
                query.AddRange(ParseInputStream(request.InputStream, request.ContentEncoding));
            }

            args = args.Concat(query).ToList();

            if (Attribute.IsDefined(method, typeof(Pagination)))
            {
                args = args.Skip(1).ToList();
            }

            if (method.Name == "PostLoginData")
            {
                args.Add(string.Empty);
            }

            if (Attribute.IsDefined(method, typeof(AuthCookieRequired)))
            {
                var manager = new SessionManager();
                var sessionId = CookieManager.GetAuthenticatedCookie(request.Cookies);
                args.Add(manager.GetAccountIdFromSession(sessionId).ToString());
            }


            return parameters
                    .Select((p, i) => Convert.ChangeType(args[i], p.ParameterType))
                    .ToArray();
        }

        private static List<string> ParseInputStream(Stream stream, Encoding encoding)
        {
            using var reader = new StreamReader(stream, encoding);

            return reader.ReadToEnd()
                .Split('&')
                .Select(pair => pair.Split('='))
                .Select(pair => HttpUtility.UrlDecode(pair[1]))
                .ToList();
        }

        private static async Task LoadErrorPage(HttpListenerResponse response)
        {
            response.ContentType = "text/html";
            response.StatusCode = (int)HttpStatusCode.NotFound;

            var buffer = await File.ReadAllBytesAsync(StringConstants.ErrorPagePath);
            using var output = response.OutputStream;

            output.Write(buffer, 0, buffer.Length);
        }
    }
}
