using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace staticfile_sv.Model
{
  public class Blog
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId BlogId { get; set; }
    public string? Tiltle { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
  }
}