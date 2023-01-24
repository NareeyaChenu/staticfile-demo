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

    
}