using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace App.Serializers {

    [Table("products")]
    public class XmlProduct {

        [XmlElement("quantity")]
        public int? Quantity { get; set; }

        [XmlElement("name")]
        public string? Name { get; set; }

        [XmlElement("price")]
        public double? Price { get; set; }

        public XmlProduct() { }
    }
}
