using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Logging;

namespace PersonalPortfolio.TaxCalc.Client
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Logging.SetMinimumLevel(LogLevel.Information);
            builder.Services.ConfigureServices(builder.HostEnvironment);

            await builder.Build().RunAsync();
        }
    }
}
