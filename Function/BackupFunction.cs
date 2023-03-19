using FileBackupApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileBackupApp.Function
{
    public class BackupFunction
    {
        public static List<BackupData> GetBackupList(BackupSystem baseData, BackupSystem newData, string fromDir, string toDir)
        {
            List<BackupData> backupList = new List<BackupData>();
            foreach (FileInfo fileInfoBase in baseData.FileInfoArray)
            {
                // When the same full path exists
                if (newData.CheckExistPath(fileInfoBase.FullName, fromDir, toDir))
                {
                    // Check timestamp and update
                    if (newData.CheckUpdate(fileInfoBase.FullName, fileInfoBase.LastWriteTime, fromDir, toDir))
                    {
                        backupList.Add(new BackupData { FileName = fileInfoBase.Name, FilePath = fileInfoBase.FullName, LastWriteTime = fileInfoBase.LastWriteTime, Status = "Update" });
                    }
                }
                else
                {
                    backupList.Add(new BackupData { FileName = fileInfoBase.Name, FilePath = fileInfoBase.FullName, LastWriteTime = fileInfoBase.LastWriteTime, Status = "Create" });
                }
            }
            foreach(FileInfo fileInfoNew in newData.FileInfoArray)
            {
                // When the destination file does not exist in the source
                if(!baseData.CheckExistPath(fileInfoNew.FullName, toDir, fromDir))
                {
                    backupList.Add(new BackupData { FileName = fileInfoNew.Name, FilePath = fileInfoNew.FullName, LastWriteTime = fileInfoNew.LastWriteTime, Status = "Delete" });
                }
            }
            return backupList;
        }

        public static void CopyFile(List<BackupData> backupList, string fromDir, string toDir)
        {
            foreach(BackupData backupData in backupList)
            {
                if(backupData.Status != "Delete")
                {
                    try
                    {
                        CreateDirAndCopy(backupData.FilePath, backupData.FilePath.Replace(fromDir, toDir));
                        LogFunction.WriteLog(backupData.Status + ":" + backupData.FilePath);
                    }
                    catch (Exception e)
                    {
                        LogFunction.WriteLog("Copy failed:" + backupData.FilePath + ":" + e.ToString().Replace("\r\n", ""));
                    }
                }
                else
                {
                    try
                    {
                        File.Delete(backupData.FilePath);
                        LogFunction.WriteLog(backupData.Status + ":" + backupData.FilePath);
                    }
                    catch (Exception e)
                    {
                        LogFunction.WriteLog("Delete failed:" + backupData.FilePath + ":" + e.ToString().Replace("\r\n", ""));
                    }
                }
            }
        }

        private static void CreateDirAndCopy(string baseFilePath, string distFullPath)
        {
            string distDir = Path.GetDirectoryName(distFullPath);
            if (!Directory.Exists(distDir))
            {
                Directory.CreateDirectory(distDir);
            }
            File.Copy(baseFilePath, distFullPath, true);
        }
    }
}