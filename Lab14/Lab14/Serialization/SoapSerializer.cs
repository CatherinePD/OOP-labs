using System.Runtime.Serialization.Formatters.Soap;

namespace Lab14.Serialization
{
    public class SoapSerializer<T> : Serializer<T>
    {
        public SoapSerializer(string fileName = null) 
            : base(new SoapFormatter(), fileName)
        {
        }
    }
}
