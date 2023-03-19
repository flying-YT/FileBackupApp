using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileBackupApp
{
    public class BackupSystem
    {
        public FileInfo[] FileInfoArray { get; set; }
        private string FilesPath { get; set; }

        public BackupSystem(string _path)
        {
            FilesPath = _path;
            GetFilesInfo();
        }

        private void GetFilesInfo()
        {
            FileInfoArray = new DirectoryInfo(FilesPath).GetFiles("*", SearchOption.AllDirectories);
        }

        public bool CheckExistPath(string _path)
        {
            foreach(FileInfo fileInfo in FileInfoArray)
            {
                if(fileInfo.FullName == _path)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckExistPath(string _path, string baseDrivateName, string replaceDriveName)
        {
            foreach (FileInfo fileInfo in FileInfoArray)
            {
                if (fileInfo.FullName == _path.Replace(baseDrivateName, replaceDriveName))
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckUpdate(string _path, DateTime _lastWriteDate)
        {
            foreach(FileInfo fileInfo in FileInfoArray)
            {
                if(fileInfo.FullName == _path)
                {
                    Console.WriteLine(fileInfo.LastWriteTime + ":" + _lastWriteDate);
                    if (fileInfo.LastWriteTime != _lastWriteDate)
                    {
                        return true;
                    }
                    break;
                }
            }
            return false;
        }

        public bool CheckUpdate(string _path, DateTime _lastWriteDate, string baseDrivateName, string replaceDriveName)
        {
            foreach (FileInfo fileInfo in FileInfoArray)
            {
                if (fileInfo.FullName == _path.Replace(baseDrivateName, replaceDriveName))
                {
                    Console.WriteLine(fileInfo.LastWriteTime + ":" + _lastWriteDate);
                    if (fileInfo.LastWriteTime != _lastWriteDate)
                    {
                        return true;
                    }
                    break;
                }
            }
            return false;
        }
    }
}