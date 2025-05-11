using System.Xml.Serialization;

namespace XmlFilesUploader.Application.Dtos.Xmls
{
    [XmlRoot("Sections")]
    public class XmlSections
    {
        [XmlElement("Section")]
        public required List<XmlSection> SectionList { get; set; }
    }
}
