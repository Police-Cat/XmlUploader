using System.Xml.Serialization;
using DataAccessLayer.DbContexts;
using DataAccessLayer.Models;
using XmlFilesUploader.Application.Dtos.Xmls;
using XmlFilesUploader.Application.Dtos.XmlSectionUploader.Models;
using XmlFilesUploader.Application.Interfaces;
using Section = DataAccessLayer.Models.Section;

namespace XmlFilesUploader.Services
{
    public class XmlSectionLoader : IXmlSectionLoader
    {
        private readonly AppDbContext _dbContext;

        public XmlSectionLoader(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SectionLoadResult> LoadXmlAsync(Stream xmlStream, CancellationToken cancellationToken)
        {
            try
            {
                var sectionsFromFile = await DeserealizaAndValidateXml(xmlStream, cancellationToken);

                if (sectionsFromFile.Count > 0)
                {
                    await _dbContext.Sections.AddRangeAsync(sectionsFromFile, cancellationToken);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                }

                return SectionLoadResult.Success(sectionsFromFile.Count);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                return SectionLoadResult.Failure($"Ошибка при разборе XML: {ex.Message}");
            }
        }

        private async Task<List<Section>> DeserealizaAndValidateXml(Stream xmlStream, CancellationToken cancellationToken)
        {
            try
            {
                using var reader = new StreamReader(xmlStream);
                string xmlContent = await reader.ReadToEndAsync(cancellationToken);

                var serializer = new XmlSerializer(typeof(XmlSections));
                using var stringReader = new StringReader(xmlContent);
                var sections = serializer.Deserialize(stringReader) as XmlSections;

                var sectionsFromFile = new List<Section>();

                if (sections is null)
                    throw new InvalidDataException("Файл не соответствует ожидаемому XML-формату.");

                foreach (var sectionElem in sections.SectionList)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var paramNameSet = new HashSet<string>();
                    var validParams = new List<Param>();

                    foreach (var param in sectionElem.Params)
                    {
                        if (string.IsNullOrWhiteSpace(param.Name) || string.IsNullOrWhiteSpace(param.Value))
                        {
                            continue;
                        }

                        if (!paramNameSet.Add(param.Name))
                        {
                            continue;
                        }

                        if (_dbContext.Sections.Any(x => x.Id == sectionElem.Id))
                        {
                            continue;
                        }

                        validParams.Add(new Param { Name = param.Name, Value = param.Value, SectionId = sectionElem.Id });
                    }

                    if (validParams.Count > 0)
                    {
                        sectionsFromFile.Add(new Section { Id = sectionElem.Id, Params = validParams });
                    }
                }

                return sectionsFromFile;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidDataException("Файл не является валидным XML или имеет неверную структуру.", ex);
            }
        }
    }
}
