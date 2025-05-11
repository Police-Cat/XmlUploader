using XmlFilesUploader.Application.Dtos.XmlSectionUploader.Models;

namespace XmlFilesUploader.Application.Interfaces
{
    public interface IXmlSectionLoader
    {
        Task<SectionLoadResult> LoadXmlAsync(Stream xmlStream, CancellationToken cancellationToken);
    }
}