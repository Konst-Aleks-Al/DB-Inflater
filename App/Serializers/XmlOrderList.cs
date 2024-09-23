using System.Xml.Serialization;

namespace App.Serializers {

    [Serializable]
    [XmlRoot("orders")]
    public class XmlOrderList {

        [XmlElement("order")]
        public List<XmlOrder>? OrderList { get; set; }

        public XmlOrderList() { }
    }
}
