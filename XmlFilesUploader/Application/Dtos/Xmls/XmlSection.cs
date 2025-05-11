using System.Xml.Serialization;

namespace XmlFilesUploader.Application.Dtos.Xmls
{
    [XmlType("Section")]
    public class XmlSection
    {
        [XmlAttribute("id")]
        public Guid Id { get; set; }

        [XmlElement("Param")]
        public required List<XmlParam> Params { get; set; }
    }
}
