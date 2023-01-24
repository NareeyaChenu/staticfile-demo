using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;
using staticfile_sv.Config;
using staticfile_sv.DTOs;
using staticfile_sv.Interface;
using staticfile_sv.Model;

namespace staticfile_sv.Service
{
  public partial class BlogService : ControllerBase , IBlogService
  {
    private readonly IMongoCollection<Blog> _blogCollection ;
     public BlogService(IOptions<DatabaseSetting> dbSetting)
     {
      Console.WriteLine(JsonConvert.SerializeObject(dbSetting));
       var mongoClient = new MongoClient
       (dbSetting.Value.ConnectionString);

       var mongodb = mongoClient.GetDatabase(
        dbSetting.Value.DatabaseName
       );

       _blogCollection = mongodb.GetCollection<Blog>(
          dbSetting.Value.BlogCollectionName
       );

     }

    public ActionResult<Blog> CreateBlog(CreateBlog model)
    {
      Blog blog = new Blog();
      blog.Tiltle = model.Title;
      blog.Description = model.Description;

      _blogCollection.InsertOne(blog);
      return Ok(blog);
    }

    public ActionResult<IEnumerable<Blog>> GetBlogs()
    {
        IEnumerable<Blog> blogs = _blogCollection.Find(_ => true).ToEnumerable();

        return Ok(blogs);
    }
  }
}