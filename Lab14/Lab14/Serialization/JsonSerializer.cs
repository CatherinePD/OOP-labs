using System.IO;
using System.Runtime.Serialization.Json;

namespace Lab14.Serialization
{
    public class JsonSerializer<T>
    {
        private readonly DataContractJsonSerializer _formatter;
        private string _fileName;

        public JsonSerializer(string fileName = null) 
        {
            _formatter = new DataContractJsonSerializer(typeof(T));
            _fileName = fileName;
        }

        public void Serialize(T obj)
        {
            using (var stream = new FileStream(_fileName, FileMode.OpenOrCreate))
            {
                _formatter.WriteObject(stream, obj);
            }
        }

        public virtual T Deserialize()
        {
            using (var stream = new FileStream(_fileName, FileMode.OpenOrCreate))
            {
                return (T)_formatter.ReadObject(stream);
            }
        }


        public byte[] SerializeBytes(T obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                _formatter.WriteObject(stream, obj);
                return stream.ToArray();
            }
        }

        public T DeserializeBytes(byte[] bytes)
        {
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                return (T)_formatter.ReadObject(memoryStream);
            }
        }
    }
}
