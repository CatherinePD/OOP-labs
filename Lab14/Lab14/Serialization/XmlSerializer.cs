
using System.IO;
using System.Runtime.Serialization;

namespace Lab14.Serialization
{
    public class XmlSerializer<T>
    {
        public XmlSerializer(string fileName = null)
        {
            _formatter = new System.Xml.Serialization.XmlSerializer(typeof(T));
            _fileName = fileName;
        }

        private readonly System.Xml.Serialization.XmlSerializer _formatter;
        private string _fileName;

        public void Serialize(T obj)
        {
            using (var stream = new FileStream(_fileName, FileMode.OpenOrCreate))
            {
                _formatter.Serialize(stream, obj);
            }
        }

        public virtual T Deserialize()
        {
            using (var stream = new FileStream(_fileName, FileMode.OpenOrCreate))
            {
                return (T)_formatter.Deserialize(stream);
            }
        }

        public byte[] SerializeBytes(T obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                _formatter.Serialize(stream, obj);
                return stream.ToArray();
            }
        }

        public T DeserializeBytes(byte[] bytes)
        {
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                return (T)_formatter.Deserialize(memoryStream);
            }
        }
    }
}
