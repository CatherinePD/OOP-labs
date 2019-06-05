using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab13
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {  //1
                var logger = new PEDLog("pedlogfile.txt");
                logger.Write("Начало работы");

                //2
                var disk = new PEDDiskInfo();
                disk.GetTotalFreeSpace("D:\\");
                disk.GetFileSystemType("D:\\");
                disk.GetDrivesInfo();

                //3
                var inf = new PEDFileInfo();
                inf.GetFileFullPath("pedlogfile.txt");
                inf.GetFileSize("pedlogfile.txt");
                inf.GetFileExtension("pedlogfile.txt");
                inf.GetFileName("pedlogfile.txt");
                inf.GetFileCreationDate("pedlogfile.txt");

                //4
                var dirInfo = new PEDDirInfo();
                dirInfo.GetFilesCount("D:\\");
                dirInfo.GetCreationDate("D:\\");
                dirInfo.GetParents("C:\\Windows\\System32\\ru-RU");

                //5
                var fileMgr = new PEDFileManager();
                string infoStr;
                var info = fileMgr.GetDriveDirectoriesAndFiles("D:\\", out infoStr);
                var newDir = fileMgr.MakeDir("D:\\", "PEDInspect");
                var file = fileMgr.MakeFile(newDir, "peddirinfo.txt");
                fileMgr.WriteToFile(file, infoStr);

                fileMgr.MakeCopy(file, "peddirinfo_copy.txt");
                var renamed = fileMgr.Rename(file, "peddirinfo_renamed.txt");
                fileMgr.Remove(renamed);

                var newDir2 = fileMgr.MakeDir("D:\\", "PEDFiles");
                var files = fileMgr.GetFiles("D:\\files", "*.txt");
                fileMgr.CopyFilesToDir(files, newDir2);
                var newPath = fileMgr.MoveDirectory(newDir2, newDir);

                var archive = fileMgr.Zip(newPath, Path.Combine(newDir, "files.zip"));
                fileMgr.Unzip(archive, fileMgr.MakeDir(newDir, "Unziped"));

                //6
                Console.WriteLine($"Введите дату (в формате dd.MM.yyyy), за которую нужно получить записи:");
                var dateStr = Console.ReadLine();
                var logDate = DateTime.ParseExact(dateStr, "dd.MM.yyyy", null);

                Console.WriteLine($"Записи в log за {dateStr}:");
                foreach (var line in logger.FindLineByDate(logDate))
                {
                    Console.WriteLine(line);
                }

                var keyString = "Диск -";
                Console.WriteLine($"\nЗаписи в log по ключевому слову '{keyString}'");
                foreach (var line in logger.FindLinesByMessage(keyString))
                {
                    Console.WriteLine(line);
                }

                var dateStrat = new DateTime(2018, 12, 22, 10, 17, 0);
                var dateEnd = new DateTime(2018, 12, 22, 10, 16, 0);
                Console.WriteLine($"\nЗаписи в log с {dateStrat} по {dateEnd}:");
                foreach (var line in logger.FindLineByInterval(dateStrat, dateEnd))
                {
                    Console.WriteLine(line);
                }

                Console.Write($"\nЧисло записей: ");
                Console.WriteLine(logger.Count);

                Console.WriteLine($"Оставить записи за последний час...");
                logger.LeaveLinesByLastHour();

                Console.Write($"\nЧисло записей: ");
                Console.WriteLine(logger.Count);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
