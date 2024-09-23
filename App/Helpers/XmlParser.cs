using App.Serializers;
using System.Xml;
using System.Xml.Serialization;

namespace App.Helpers {

    public class XmlParser {

        public XmlParser() { }

        public static XmlOrderList? TryParse(string filePath) {

            try {
                XmlSerializer serializer = new(typeof(XmlOrderList));
                using XmlReader xmlReader = XmlReader.Create(filePath);
                XmlOrderList? orders = serializer.Deserialize(xmlReader) as XmlOrderList;
                xmlReader.Dispose();
                return orders;
            }
            catch (InvalidOperationException) {

                Console.WriteLine($"Файл {filePath} содержит дефект или не соответствует схеме.");
                return null;
            }
        }
    }
}
