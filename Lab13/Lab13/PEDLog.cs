using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab13
{
    public class PEDLog
    {
        private readonly string _path;

        private const string DateTimeFormat = "dd.MM.yyyy HH:mm";

        private IEnumerable<LogEntry> Entries => ReadLogEntries();

        public int Count => Entries.Count();

        private int LastEntryId { get; set; }

        public PEDLog(string path)
        {
            _path = path;
            ReadLogEntries(); // считывание текущих значений в файле
        }

        public void Write(string message)
        {
             StreamWriter writer;

            if (!File.Exists(_path))
                writer = File.CreateText(_path);
            else
               writer = File.AppendText(_path);

            message = message.Replace("\n", "").Replace("\r", "");
            var entry = new LogEntry { Message = message.Replace("\n", ""), Time = DateTime.Now, Id = ++LastEntryId };

            writer.WriteLine(entry.ToString());
            writer.Close();
        }

        public IEnumerable<LogEntry> FindLinesByMessage(string message)
        {
            var result = Entries.Where(e => e.Message.Contains(message));
            return result.ToList();
        }

        public IEnumerable<LogEntry> FindLineByDate(DateTime dateTime)
        {
            var result = Entries.Where(e => e.Time.Date.Equals(dateTime.Date));
            return result.ToList();
        }

        public IEnumerable<LogEntry> FindLineByInterval(DateTime begin, DateTime end)
        {
            var result = Entries.Where(e => e.Time >= begin && e.Time <= end);
            return result.ToList();
        }

        public void LeaveLinesByLastHour()
        {
            var endTime = DateTime.Now.AddHours(-1);
            var startTime = DateTime.MinValue;

            foreach (var line in FindLineByInterval(startTime, endTime ))
            {
                RemoveLine(line.Id); //delete с мин времени до предпоследнего часа
            }
        }

        public void RemoveLine(int id)
        {
            var entries = ReadLogEntries().ToList();
            var entryToRemove = entries.FirstOrDefault(e => e.Id == id); // находим запись для удаления

            if (entryToRemove == null) return;

            entries.Remove(entryToRemove); //удаляем запись из списка

            using (StreamWriter writer = new StreamWriter(_path, false))
            {
                entries.ForEach(e => writer.WriteLine(e.ToString()));  //перезаписываем новый список в файл
            }
        }

        private IEnumerable<LogEntry> ReadLogEntries()
        {
            var result = new List<LogEntry>();

            if (!File.Exists(_path)) return result;
            using (StreamReader reader = new StreamReader(_path))
            {
                string line;
                while ((line = reader.ReadLine()) != null) // читаем построчно
                {
                    result.Add(GetEntry(line));
                }
            }

            if(result.Any())
                LastEntryId = result.Max(e=>e.Id);  // записываем последний Id

            return result;
        }

        private LogEntry GetEntry(string line)  // получение LogEntry из строки
        {
                var fragments = line.Remove(line.Length - 1, 1) // удаление последнего символа
                                    .Remove(0, 1)// удаление первого символа
                                    .Split(new [] { "]:[" }, StringSplitOptions.None); // разбиение строки по фрагментам

                var id = int.Parse(fragments[0]);
                var time = DateTime.ParseExact(fragments[1], DateTimeFormat, null);
                var message = fragments[2];

                return new LogEntry {Message = message, Id = id, Time = time};
        }

        public class LogEntry
        {
            public int Id { get; set; }
            public string Message { get; set; }
            public DateTime Time { get; set; }

            public override string ToString()
            {
                return $"[{Id}]:[{Time.ToString(DateTimeFormat)}]:[{Message}]";
            }
        }

    }
}
