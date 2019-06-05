using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab13
{
    public class PEDFileManager
    {
        private PEDLog _logger;
        public PEDFileManager()
        {
            _logger = new PEDLog("pedlogfile.txt");
        }

        public string MakeDir(string driveName, string dirName)
        {
            try
            {
                var path = Path.Combine(driveName, dirName);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    _logger.Write($"Создана директория {path}");
                }
                else
                    _logger.Write($"Директория {path} уже существует");

                return path;
            }
            catch (Exception e)
            {
                _logger.Write($"Ошибка при создании директории {dirName} на диске {driveName} - {e.Message}");
            }
            return "";
        }

        public string MakeFile(string location, string filename)
        {
            try
            {
                var path = Path.Combine(location, filename);

                if (!File.Exists(path))
                {
                    var file = File.Create(path);
                    file.Close();
                    _logger.Write($"Создан файл {path}");
                }
                else
                    _logger.Write($"Файл {path} уже существует");

                return path;
            }
            catch (Exception e)
            {
                _logger.Write($"Ошибка при создании файла {filename} в директории {location} - {e.Message}");
            }
            return "";
        }

        public void WriteToFile(string path, string text)
        {
            if (File.Exists(path))
            {
                try
                {
                    using (var writer = new StreamWriter(path))
                    {
                        writer.WriteLine(text);
                        _logger.Write($"Запись в файл {path}");
                    }
                }
                catch (Exception e)
                {
                    _logger.Write($"Ошибка при записи в файл {path} - {e.Message}");
                }
            }
            else 
                _logger.Write($"Ошибка - файла не существует");
        }

        public void MakeCopy(string path, string newFileName)
        {
            if (File.Exists(path))
            {
                try
                {
                    var info = new FileInfo(path);
                    var newFilePath = Path.Combine(info.DirectoryName, newFileName);

                    File.Copy(path, newFilePath);

                    _logger.Write($"Скопирован файл {path}, имя копии: {newFileName}");
                }
                catch (Exception e)
                {
                    _logger.Write($"Ошибка при создании копии {path} - {e.Message}");
                }
            }
            else
                _logger.Write($"Ошибка - файла {path} не существует");
        }

        public string Rename(string path, string newName)
        {
            if (File.Exists(path))
            {
                try
                {
                    var info = new FileInfo(path);
                    var newFilePath = Path.Combine(info.DirectoryName, newName);

                    File.Move(path, newFilePath);

                    _logger.Write($"Переименован файл {path}, новое имя: {newName}");
                    return newFilePath;
                }
                catch (Exception e)
                {
                    _logger.Write($"Ошибка - при переименовании файла {path} - {e.Message}");
                }
            }
            else
                _logger.Write($"Ошибка - файла {path} не существует");
            return path;
        }

        public void Remove(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                    _logger.Write($"Файл {path} был удален");
                }
                catch (Exception e)
                {
                    _logger.Write($"Ошибка при удалении файла {path} - {e.Message}");
                }
            }
            else
                _logger.Write($"Ошибка - файла {path} не существует");
        }

        public IEnumerable<string> GetFiles(string path, string pattern)
        {
            try
            {
                return Directory.GetFiles(path, pattern, SearchOption.TopDirectoryOnly);
            }
            catch (Exception e)
            {
                _logger.Write($"Ошибка при получении файлов по адресу {path} - {e.Message} ");
            }
            return null;
        }

        public void CopyFilesToDir(IEnumerable<string> files, string path)
        {
            try
            {
                foreach (var file in files)
                {
                    var info = new FileInfo(file);
                    File.Copy(file, Path.Combine(path, info.Name));
                }
                _logger.Write($"Скопировано {files.Count()} файлов в директорию {path}");
            }
            catch (Exception e)
            {
                _logger.Write($"Ошибка при копировании в директорию {path} - {e.Message}");
            }
        }

        public string MoveDirectory(string sourceDirName, string destDirName)
        {
            try
            {
                var info = new DirectoryInfo(sourceDirName);
                var newPath = Path.Combine(destDirName, info.Name);

                info.MoveTo(newPath);

                _logger.Write($"Директория {sourceDirName}  скопирована в {destDirName}");

                return newPath;
            }
            catch (Exception e)
            {
                _logger.Write($"Ошибка при перемещении директории {sourceDirName} в {destDirName} - {e.Message}");
            }
            return "";
        }

        public string Zip(string path, string archiveName)
        {
            try
            {
                ZipFile.CreateFromDirectory(path, archiveName);
                _logger.Write($"Создан архив {archiveName}");

                return archiveName;
            }
            catch (Exception e)
            {
                _logger.Write($"Ошибка при создании архива {archiveName} - {e.Message}");
            }
            return null;
        }

        public void Unzip(string archivePath, string destinationPath)
        {
            try
            {
                ZipFile.ExtractToDirectory(archivePath, destinationPath);
                _logger.Write($"Распакован архив {archivePath} в {destinationPath}");
            }
            catch (Exception e)
            {
                _logger.Write($"Ошибка при разархивации {archivePath} - {e.Message}");
            }
        }

        public Tuple<string[], string[]> GetDriveDirectoriesAndFiles(string driveName, out string output)
        {
            try
            {
                var files = Directory.GetFiles(driveName, "*", SearchOption.TopDirectoryOnly);
                var dirs = Directory.GetDirectories(driveName, "*", SearchOption.TopDirectoryOnly);

                output = $"Директории на диске {driveName}:\r\n";
                foreach (var dir in dirs)
                    output += $"  {dir}\r\n";

                output += $"Файлы на диске {driveName}:\r\n";
                foreach (var file in files)
                    output += $"  {file}\r\n";

                _logger.Write($"Найдено на диске {driveName} - {dirs.Length} директорий, {files.Length} файлов");

                return new Tuple<string[], string[]>(files, dirs);
            }
            catch (Exception e)
            {
                _logger.Write($"Ошибка при получении информации о файлах и папках на диске {driveName} - {e.Message}");
            }
            output = null;
            return null;
        }
    }
}
