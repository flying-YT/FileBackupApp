using System;
using System.Collections.Generic;
using System.Text;

namespace FileBackupApp.Model
{
    public class BackupData
    {
        public string FileName { set; get; }
        public string FilePath { set; get; }
        public DateTime LastWriteTime { set; get; }
        public string Status { set; get; }
    }
}