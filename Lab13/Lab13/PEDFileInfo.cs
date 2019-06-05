using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab13
{
    public class PEDFileInfo
    {
        private PEDLog _logger;
        public PEDFileInfo()
        {
            _logger = new PEDLog("pedlogfile.txt");
        }

        public float GetFileSize(string path)
        {
            try
            {
                var fileInfo = new FileInfo(path);

                var size = fileInfo.Length / Convert.ToSingle(1024); //KB

                _logger.Write($"Размер файла {path}: {size} KB");
                return size;
            }
            catch (Exception e)
            {
                _logger.Write($"Ошибка получения файла {path} - {e.Message}");
            }
            return -1;
        }

        public string GetFileFullPath(string path)
        {
            try
            {
                var fileInfo = new FileInfo(path);
                var fullPath = fileInfo.FullName;

                _logger.Write($"Полный путь файла {path}: {fullPath}");
                return fullPath;
            }
            catch (Exception e)
            {
                _logger.Write($"Ошибка получения файла {path} - {e.Message}");
            }
            return "";
        }

        public string GetFileExtension(string path)
        {
            try
            {
                var fileInfo = new FileInfo(path);

                var ext = fileInfo.Extension;

                _logger.Write($"Разрешение файла {path}: {ext}");
                return ext;
            }
            catch (Exception e)
            {
                _logger.Write($"Ошибка получения файла {path} - {e.Message}");
            }
            return "";
        }

        public string GetFileName(string path)
        {
            try
            {
                var fileInfo = new FileInfo(path);

                var name = fileInfo.Name;

                _logger.Write($"Имя файла {path}: {name}");
                return name.Replace($"{fileInfo.Extension}", ""); //вернуть без расширения
            }
            catch (Exception e)
            {
                _logger.Write($"Ошибка получения файла {path} - {e.Message}");
            }
            return "";
        }

        public string GetFileCreationDate(string path)
        {
            try
            {
                var fileInfo = new FileInfo(path);

                var date = fileInfo.CreationTime;

                _logger.Write($"Время создания файла {path}: {date:dd.MM.yyyy HH:mm}");
                return date.ToString("dd.MM.yyyy HH:mm");
            }
            catch (Exception e)
            {
                _logger.Write($"Ошибка получения файла {path} - {e.Message}");
            }
            return "";
        }
    }
}
