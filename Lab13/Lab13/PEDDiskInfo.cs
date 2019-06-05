using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab13
{
    public class PEDDiskInfo
    {
        private PEDLog _logger;

        private DriveInfo[] Drives => DriveInfo.GetDrives();

        public PEDDiskInfo()
        {
            _logger = new PEDLog("pedlogfile.txt");
        }

        public string GetFileSystemType(string driveName)
        {
            string format = "";
            try
            {
                var drive = GetDrive(driveName);

                format = drive.DriveFormat;
                _logger.Write($"Тип файловой системы: {format}");
                return drive.DriveFormat;
            }
            catch (Exception e)
            {
                _logger.Write($"Ошибка получения диска с именем '{driveName}' - {e.Message}");
            }
            
            return format;
        }

        public float GetTotalFreeSpace(string driveName)
        {
            try
            {
                var drive = GetDrive(driveName);

                float space = drive.AvailableFreeSpace /Convert.ToSingle(1024 * 1024 * 1024); //из байт в GB
                _logger.Write($"Свободное место на диске {driveName} - {space} GB");
                return space;
            }
            catch (Exception e)
            {
                _logger.Write($"Ошибка получения диска с именем '{driveName}' - {e.Message}");
            }
            return -1;
        }

        public string GetDrivesInfo()
        {
            string result = "";
            try
            {
                foreach (var drive in Drives)
                {
                    if (drive.IsReady)
                    {
                        var driveSize = drive.TotalSize / Convert.ToSingle(1024 * 1024 * 1024);
                        var driveFreeSpace = drive.AvailableFreeSpace / Convert.ToSingle(1024 * 1024 * 1024);
                        var driveInfo = $"Имя: {drive.Name}, Объём: {driveSize} GB, Доступно: {driveFreeSpace} GB, Метка тома: {drive.VolumeLabel}";
                        _logger.Write($"Диск - {driveInfo}");

                        result += driveInfo + "\n";
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Write($"Ошибка получения информации о дисках - {e.Message}");
            }
           return result;
        }

        private DriveInfo GetDrive(string driveName)
        {
            try
            {
                return Drives.FirstOrDefault(d => d.IsReady && d.Name == driveName);
            }
            catch (Exception e)
            {
                _logger.Write($"Ошибка - невозможно найти диск с именем '{driveName}' - {e.Message}");
                throw e;
            }
        }

    }
}
