using Application.Interfaces;
using Serilog.Core;

namespace WebAPI.BackgroundServices;

/// <summary>
/// Cleaning orders that are unfinished
/// </summary>
public class OrderCleaningBackgroundService : BackgroundService
{
    private readonly ILogger<OrderCleaningBackgroundService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly int cleaningPeriod = 10;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="serviceScopeFactory "></param>
    /// <param name="logger"></param>
    public OrderCleaningBackgroundService(IServiceScopeFactory serviceScopeFactory, ILogger<OrderCleaningBackgroundService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    /// <summary>
    /// Start background service
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await DoWork(stoppingToken);
    }

    private async Task DoWork(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Unfinished orders cleaning service is starting");

        while (true)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedProcessingService =
                        scope.ServiceProvider
                            .GetRequiredService<IOrderService>();

                    DateTime dateTime = DateTime.UtcNow.AddMinutes(-cleaningPeriod);
                    int count = await scopedProcessingService.CleanUnfinishedOrders(dateTime);

                    if (count > 0)
                    {
                        _logger.LogInformation($"Unfinished orders {count} cleared");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cleaning failed {ex.Message}");
            }

            await Task.Delay(5000);
        }
    }

    /// <summary>
    /// Stop background service
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Unfinished orders cleaning service is stopping");

        await base.StopAsync(stoppingToken);
    }
}
