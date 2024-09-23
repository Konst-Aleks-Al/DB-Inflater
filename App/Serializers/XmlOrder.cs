using System.Xml.Serialization;

namespace App.Serializers {

    [Serializable()]
    public class XmlOrder {

        [XmlElement("no")]
        public int Id { get; set; }

        private DateOnly _created;

        [XmlElement("reg_date")]
        public string Created {
            get { return _created.ToString(); }
            set { _created = DateOnly.Parse(value); }
        }

        [XmlElement("sum")]
        public double Sum { get; set; }

        [XmlElement("product")]
        public List<XmlProduct>? ProductList { get; set; }

        [XmlElement("user")]
        public XmlUser? User { get; set; }

        public XmlOrder() { }
    }
}
