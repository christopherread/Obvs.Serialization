using System.IO;
using System.Xml.Serialization;

namespace Obvs.Serialization.Xml
{
    public class XmlMessageDeserializer<TMessage> : MessageDeserializerBase<TMessage>
        where TMessage : class
    {
        private readonly XmlSerializer _xmlSerializer;

        public XmlMessageDeserializer()
        {
            _xmlSerializer = new XmlSerializer(typeof(TMessage));
        }

        public override TMessage Deserialize(object obj)
        {
            using (MemoryStream ms = new MemoryStream((byte[])obj))
            {
                return Deserialize(ms);
            }
        }

        public override TMessage Deserialize(Stream stream)
        {
            using (TextReader reader = new StreamReader(stream, XmlSerializerDefaults.Encoding))
            {
                return (TMessage)_xmlSerializer.Deserialize(reader);
            }
        }
    }
}