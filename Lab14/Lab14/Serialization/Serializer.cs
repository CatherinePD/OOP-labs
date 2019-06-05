using System.IO;
using System.Runtime.Serialization;

namespace Lab14.Serialization
{
    public abstract class Serializer<T>
    {
        protected IFormatter Formatter { get; set; }

        protected string _fileName;

        protected Serializer(IFormatter formatter = null, string fileName = null)
        {
            Formatter = formatter;
            _fileName = fileName;
        }

        public virtual void Serialize(T obj)
        {
            using (var stream = new FileStream(_fileName, FileMode.OpenOrCreate))
            {
                Formatter.Serialize(stream, obj);
            }
        }

        public virtual T Deserialize()
        {
            using (var stream = new FileStream(_fileName, FileMode.OpenOrCreate))
            {
                return (T)Formatter.Deserialize(stream);
            }
        }

        public byte[] SerializeBytes(T obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Formatter.Serialize(stream, obj);
                return stream.ToArray();
            }
        }

        public T DeserializeBytes(byte[] bytes)
        {
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                return (T)Formatter.Deserialize(memoryStream);
            }
        }
    }
}
