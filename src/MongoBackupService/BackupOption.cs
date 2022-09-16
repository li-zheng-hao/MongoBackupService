namespace MongoBackupService;

public class BackupOption
{
 
    public string DbUsername { get; set; }
    public int FullBackupInterval { get; set; }
    public string BackupDir { get; set; }
    public int IncrementalBackupInterval { get; set; }
    public string DbName { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string Password { get; set; }
}