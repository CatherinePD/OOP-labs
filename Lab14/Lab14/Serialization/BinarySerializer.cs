using System.Runtime.Serialization.Formatters.Binary;

namespace Lab14.Serialization
{
    public class BinarySerializer<T>: Serializer<T>
    {
        public BinarySerializer(string filename=null)
            :base(new BinaryFormatter(), filename)
        {
        }
    }
}
