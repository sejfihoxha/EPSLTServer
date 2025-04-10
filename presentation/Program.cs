using EPSLTTaskServer.Application.Interfaces;

public class Program
{
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                var configuration = context.Configuration;
                services.AddDiscountCodeDb(configuration);
                services.AddApplicationServices();
            })
            .Build();

        var server = new TcpServer(5000, host.Services.GetRequiredService<IDiscountService>());
        server.Start();
    }
}
