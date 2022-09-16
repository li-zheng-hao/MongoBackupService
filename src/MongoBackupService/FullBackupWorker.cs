namespace MongoBackupService;

public class FullBackupWorker : BackgroundService
{
    private readonly ILogger<FullBackupWorker> _logger;
    private readonly IConfiguration _config;
    /// <summary>
    /// 同步时间间隔 秒
    /// </summary>
    private readonly int _interval;

    public FullBackupWorker(ILogger<FullBackupWorker> logger,IConfiguration configuration)
    {
        _config = configuration;
        _logger = logger;
        _interval=Convert.ToInt32( configuration["FullBackupInterval"]);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("全量备份任务开始执行 时间: {time}", DateTime.Now);
            await Task.Delay(TimeSpan.FromSeconds(_interval), stoppingToken);
        }
    }
}