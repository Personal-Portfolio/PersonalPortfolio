using System.Threading.Tasks;
using Fluxor;
using Microsoft.Extensions.Logging;

namespace PersonalPortfolio.TaxCalc.Client.Store
{
    public class LoggingMiddleware : Middleware
    {
        public LoggingMiddleware(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("Client application state");
        }

        private readonly ILogger _logger;

        public override Task InitializeAsync(IStore store)
        {
            _logger.LogInformation("Store initialized.");
            return Task.CompletedTask;
        }

        public override void BeforeDispatch(object action)
        {
            _logger.LogInformation($"Starting processing the action({action.GetType()})");
        }

        public override void AfterDispatch(object action)
        {
            _logger.LogInformation($"Finished processing the action({action.GetType()})");
        }
    }
}
