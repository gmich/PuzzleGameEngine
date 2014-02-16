using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace PuzzleEngineAlpha.Mappers
{
    public class InputXMLMapper : IMapper
    {

        public void Load(object entity)
        {
            //load logic using LinQ
        }
        public void Save(object entity)
        {
            //save logic using LinQ
        }

        public string Serialize(object objectToSerialize)
        {
            MemoryStream mem = new MemoryStream();
            XmlSerializer ser = new XmlSerializer(objectToSerialize.GetType());
            ser.Serialize(mem, objectToSerialize);
            ASCIIEncoding ascii = new ASCIIEncoding();
            return ascii.GetString(mem.ToArray());
        }

        public object Deserialize(Type typeToDeserialize, string xmlString)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(xmlString);
            MemoryStream mem = new MemoryStream(bytes);
            XmlSerializer ser = new XmlSerializer(typeToDeserialize);
            return ser.Deserialize(mem);
        }
    }
}
