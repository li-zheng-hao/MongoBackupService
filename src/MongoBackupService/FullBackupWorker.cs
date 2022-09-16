using System.Management.Automation;
using System.Runtime.InteropServices.ComTypes;

namespace MongoBackupService;

public class FullBackupWorker : BackgroundService
{
    private readonly ILogger<FullBackupWorker> _logger;
    private readonly IConfiguration _config;

    /// <summary>
    /// 同步时间间隔 秒
    /// </summary>
    private readonly int _interval;

    private readonly string _fullBackupPath;
    private readonly BackupOption _option;

    public FullBackupWorker(ILogger<FullBackupWorker> logger, IConfiguration configuration,BackupOption option)
    {
        _option = option;
        _config = configuration;
        _logger = logger;
        _interval = Convert.ToInt32(configuration["FullBackupInterval"]);
        var _dir = configuration["BackupDir"];
        _fullBackupPath = Path.Combine(_dir, "fullbackup");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("全量备份任务开始执行 时间: {time}", DateTime.Now);
            await Execute();
            await Task.Delay(TimeSpan.FromSeconds(_interval), stoppingToken);
        }
    }

    private async Task Execute()
    {
        if (Directory.Exists(_fullBackupPath) == false)
        {
            Directory.CreateDirectory(_fullBackupPath);
        }

        var script=string.Format("mysqldump --u {0}, -p {1} --db {2} --out {3} --gzip","sda");
        await CommonUtil.RunScript(script);
    }
}