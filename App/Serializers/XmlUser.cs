using System.Xml.Serialization;

namespace App.Serializers {

    public class XmlUser {

        [XmlElement("fio")]
        public string? FullName { get; set; }

        [XmlElement("email")]
        public string? Email { get; set; }

        public XmlUser() { }
    }
}
