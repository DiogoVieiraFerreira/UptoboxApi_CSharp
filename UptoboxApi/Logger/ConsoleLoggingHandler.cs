using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace UptoboxApi.Logger
{
    public class ConsoleLoggingHandler : DelegatingHandler
    {
        public enum LogginHandlerInformation
        {
            Request,
            Response,
            All,
        }

        private LogginHandlerInformation _logginHandlerInformation;
        
        public ConsoleLoggingHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }
        public ConsoleLoggingHandler(HttpMessageHandler innerHandler, LogginHandlerInformation logginHandlerInformation)
            : base(innerHandler)
        {
            _logginHandlerInformation = logginHandlerInformation;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_logginHandlerInformation is LogginHandlerInformation.Request or LogginHandlerInformation.All)
                await ShowRequest(request);
            
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            if (_logginHandlerInformation is LogginHandlerInformation.Response or LogginHandlerInformation.All)
                await ShowResponse(response);

            return response;
        }

        private async Task ShowRequest(HttpRequestMessage request)
        {
            Console.WriteLine("Request:");
            Console.WriteLine(request.ToString());
            
            if (request.Content != null && request.Method != HttpMethod.Post)
            {
                Console.WriteLine(await request.Content.ReadAsStringAsync());
            }
            Console.WriteLine();
        }

        private async Task ShowResponse(HttpResponseMessage response)
        {
            Console.WriteLine("Response:");
            Console.WriteLine(response.ToString());
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            Console.WriteLine();
        }
    }
}