namespace staticfile_sv.Model
{
    public class StaticFileModel
    {
        public virtual byte[]? ProductFile { get; set; }
    }

    public class GetStaticFileModel
    {
        public string? Name { get; set; }
        public string? Url { get; set; }
    }
}