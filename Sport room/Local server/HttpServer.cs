using System.Net;
using System.Text.Json;

namespace Local_server
{
    internal class HttpServer : IDisposable
    {
        private readonly HttpListener _listener;
        private ServerSettings? settings;
        private ServerStatus status;

        public ServerStatus Status => status;

        public HttpServer()
        {
            _listener = new HttpListener();
            status = ServerStatus.Stopped;
        }

        public void Start()
        {
            if (status == ServerStatus.Started)
            {
                Console.WriteLine("Сервер уже начал свою работу");
                return;
            }

            settings = JsonSerializer.Deserialize<ServerSettings>(File.ReadAllBytes("./serverSettings.json"));

            _listener.Prefixes.Clear();
            _listener.Prefixes.Add($"http://localhost:{settings.Port}/");

            Console.WriteLine("Запуск сервера...");
            _listener.Start();
            Console.WriteLine($"Сервер запущен http://localhost:{settings.Port}/");

            status = ServerStatus.Started;

            ListenAsync();
        }

        public void Stop()
        {
            if (status == ServerStatus.Stopped)
            {
                Console.WriteLine("Сервер уже завершил работу");
                return;
            }

            Console.WriteLine("Остановка сервера...");
            _listener.Stop();
            Console.WriteLine("Сервер остановлен");

            status = ServerStatus.Stopped;
        }

        public async Task ListenAsync()
        {
            while (true)
            {
                var context = await _listener.GetContextAsync();
                var request = context.Request;
                using var response = context.Response;

                await ResponseMaker.MakeAsync(request, response, settings);
            }
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
