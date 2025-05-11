using System.Xml.Serialization;

namespace XmlFilesUploader.Application.Dtos.Xmls
{
    [XmlType("Param")]
    public class XmlParam
    {
        [XmlAttribute("name")]
        public required string Name { get; set; }

        [XmlAttribute("value")]
        public required string Value { get; set; }
    }
}
