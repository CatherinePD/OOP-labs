using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab13
{
    public class PEDDirInfo
    {
        private PEDLog _logger;

        public PEDDirInfo()
        {
            _logger = new PEDLog("pedlogfile.txt");
        }

        public int GetFilesCount(string directoryPath)
        {
            try
            {
                DirectoryInfo d = new DirectoryInfo(directoryPath);
                var filesCount = d.GetFiles("*", SearchOption.TopDirectoryOnly).Length; // 1 - поиск всех файлов, 2 - поиск без захода в поддиректории

                _logger.Write($"Файлов в директории {d.Name}: {filesCount}");
                return filesCount;
            }
            catch (Exception e)
            {
                _logger.Write($"Ошибка при получении директории {directoryPath} - {e.Message}");
            }
            return -1;
        }

        public string GetCreationDate(string directoryPath)
        {
            try
            {
                DirectoryInfo d = new DirectoryInfo(directoryPath);
                var creationDate = d.CreationTime.ToString("dd.MM.yyyy HH:mm");

                _logger.Write($"Время создания директории {d.Name}: {creationDate}");
                return creationDate;
            }
            catch (Exception e)
            {
                _logger.Write($"Ошибка при получении директории {directoryPath} - {e.Message}");
            }
            return "";
        }

        public string GetParents(string directoryPath)
        {
            try
            {
                DirectoryInfo d = new DirectoryInfo(directoryPath);
                List<DirectoryInfo> parents = new List<DirectoryInfo>();

                DirectoryInfo parent = d.Parent;

                while (parent != null)
                {
                    parents.Add(parent);
                    parent = parent.Parent;
                }
                string parentsStr = string.Join(" => ", parents.Select(p => p.Name));

                _logger.Write($"Родительские директории для {d.Name}: {parentsStr}");
            }
            catch (Exception e)
            {
                _logger.Write($"Ошибка при получении директории {directoryPath} - {e.Message}");
            }
            return "";
        }
    }
}
