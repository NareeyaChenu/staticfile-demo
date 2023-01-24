using Newtonsoft.Json;

namespace staticfile_sv.DTOs
{
    public class CreateBlog
    {

        public string? Title { get; set; }
        public byte[]? FileData { get; set; }
        public string? FileType { get; set; }
        public string? Description { get; set; }
    }


    public class CreateFile 
    {
        public string? Base64EncodedFile { get; set; }
        public string? Type { get; set; }
    }

    public class StaticResModel
    {
        public string? imageUrl { get; set; }
        public string? signature { get; set; }
        public string? errorCode { get; set; }
    }

    
}