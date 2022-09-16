using MongoBackupService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<FullBackupWorker>();
        services.AddHostedService<IncrementalBackupWorker>();
    })
    .ConfigureServices(it =>
    {
        it.AddSingleton<BackupOption>(it =>
        {
            var configuration = it.GetService<IConfiguration>();
            var config = configuration.Get<BackupOption>();
            return config;
        });
    })
    .Build();
await host.RunAsync();