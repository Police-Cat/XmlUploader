namespace XmlFilesUploader.Application.Dtos
{
    namespace XmlSectionUploader.Models
    {
        public class SectionLoadResult
        {
            public int SavedCount { get; set; }
            public string? ErrorMessage { get; set; }

            public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);

            public static SectionLoadResult Success(int saved) => new() { SavedCount = saved };

            public static SectionLoadResult Failure(string error) => new() { ErrorMessage = error };
        }

    }
}
