using System;
using System.Collections.Concurrent;
using System.IO;
using System.Xml.Serialization;

namespace Obvs.Serialization.Xml
{
    public class XmlMessageSerializer : IMessageSerializer
    {
        private readonly ConcurrentDictionary<Type, XmlSerializer> _serializers = new ConcurrentDictionary<Type, XmlSerializer>();

        public object Serialize(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Serialize(ms, obj);

                return ms.ToArray();
            }
        }

        public void Serialize(Stream stream, object obj)
        {
            using (TextWriter writer = new StreamWriter(stream, XmlSerializerDefaults.Encoding))
            {
                Serializer(obj.GetType()).Serialize(writer, obj);
            }
        }

        private XmlSerializer Serializer(Type type)
        {
            return _serializers.GetOrAdd(type, t => new XmlSerializer(t));
        }
    }
}