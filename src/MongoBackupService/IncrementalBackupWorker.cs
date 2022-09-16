namespace MongoBackupService;


public class IncrementalBackupWorker : BackgroundService
{
    private readonly ILogger<IncrementalBackupWorker> _logger;
    private readonly IConfiguration _config;
    private readonly int _interval;
    private readonly string _dir;

    public IncrementalBackupWorker(ILogger<IncrementalBackupWorker> logger,IConfiguration configuration)
    {
        _config = configuration;
        _logger = logger;
        _interval=Convert.ToInt32( configuration["IncrementalBackupInterval"]);
        _dir= configuration["BackupDir"];

    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("增量备份任务开始执行 时间: {time}", DateTime.Now);
            await Task.Delay(TimeSpan.FromSeconds(_interval), stoppingToken);
        }
    }
}