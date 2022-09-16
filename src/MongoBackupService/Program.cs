using MongoBackupService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<FullBackupWorker>();
        services.AddHostedService<IncrementalBackupWorker>();
    })
    .Build();

await host.RunAsync();