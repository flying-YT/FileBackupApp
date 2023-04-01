using FileBackupApp;
using FileBackupApp.Function;

string version = "1.1";

LogFunction.LogListInstance();
LogFunction.WriteLog("version:" + version);
LogFunction.WriteLog("Backup Start");
ArgsFunction argsFunction = null;

// Make Args
try
{
    argsFunction = new ArgsFunction(Environment.GetCommandLineArgs());
    LogFunction.WriteLog("Completion of reading arguments");
}
catch(Exception e)
{
    LogFunction.WriteLog(e.ToString());
    LogFunction.MakeLogFile(argsFunction.GetDictionaryValue("/logDir"), "log");
    return;
}

// Get backup list
BackupSystem oldBackup = new BackupSystem(argsFunction.GetDictionaryValue("/fromDir"));
LogFunction.WriteLog("Loading of backup source completed");

BackupSystem newBackup = new BackupSystem(argsFunction.GetDictionaryValue("/toDir"));
LogFunction.WriteLog("Loading of backup destination completed");

var backupList = BackupFunction.GetBackupList(oldBackup, newBackup, argsFunction.GetDictionaryValue("/fromDir"), argsFunction.GetDictionaryValue("/toDir"));
BackupFunction.CopyFile(backupList, argsFunction.GetDictionaryValue("/fromDir"), argsFunction.GetDictionaryValue("/toDir"));

var deleteFolderList = BackupFunction.GetDeleteFolder(oldBackup, newBackup, argsFunction.GetDictionaryValue("/fromDir"), argsFunction.GetDictionaryValue("/toDir"));
deleteFolderList.Reverse();
BackupFunction.DeleteEmptyFolder(deleteFolderList);

LogFunction.WriteLog("Finish");

LogFunction.MakeLogFile(argsFunction.GetDictionaryValue("/logDir"), "log");




