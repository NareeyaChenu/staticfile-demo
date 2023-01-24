using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using staticfile_sv.Config;
using staticfile_sv.DTOs;
using staticfile_sv.Interface;
using staticfile_sv.Model;
using System.Text.Json;



namespace staticfile_sv.Service
{
  public partial class BlogService : ControllerBase, IBlogService
  {
    private readonly IMongoCollection<Blog> _blogCollection;
    private readonly IConfiguration _configuration;

    public BlogService(IOptions<DatabaseSetting> dbSetting, IConfiguration configuration)
    {
      var mongoClient = new MongoClient
      (dbSetting.Value.ConnectionString);

      var mongodb = mongoClient.GetDatabase(
       dbSetting.Value.DatabaseName
      );

      _blogCollection = mongodb.GetCollection<Blog>(
         dbSetting.Value.BlogCollectionName
      );
      this._configuration = configuration;
    }

    public async Task<ActionResult<Blog>> CreateBlog(CreateBlog model)
    {
      HttpClient client = new HttpClient();
      StaticResModel? resModel = new StaticResModel();
      string url = _configuration["Static:Endpoints"];
      Blog blog = new Blog();
      blog.Tiltle = model.Title;
      blog.Description = model.Description;

      CreateFile requestBody = new CreateFile
      {
        Base64EncodedFile = Convert.ToBase64String(model.FileData!),
        Type = model.FileType

      };
      string signature = ValidateSignature(requestBody);
      string body = JsonSerializer.Serialize(requestBody);
      var content = new StringContent(body, Encoding.UTF8, "application/json");
      client.DefaultRequestHeaders.Add("x-static-signature", signature);

      HttpResponseMessage response = new HttpResponseMessage();
      response = await client.PostAsync(url, content);

      string result = await response.Content.ReadAsStringAsync();

      resModel = JsonSerializer.Deserialize<StaticResModel>(result);

      blog.ImageUrl = resModel!.imageUrl;





      _blogCollection.InsertOne(blog);



      return Ok(result);
    }

    public ActionResult<IEnumerable<Blog>> GetBlogs()
    {
      IEnumerable<Blog> blogs = _blogCollection.Find(_ => true).ToEnumerable();


      return Ok(blogs);
    }

    public string ValidateSignature(object model)
    {
      string signature = "";

      string staticSecret = _configuration["Static:Secret"];

      // create a new instance of HMACSHA256
      var key = Encoding.UTF8.GetBytes(staticSecret);
      var hmac = new HMACSHA256(key);

      // Compute the HMAC of the request body
      // the request body need to be string
      var requestBody = JsonSerializer.Serialize(model);
      var bodyBytes = Encoding.UTF8.GetBytes(requestBody);
      var hmacBytes = hmac.ComputeHash(bodyBytes);

      signature = System.Convert.ToBase64String(hmacBytes);

      return signature;
    }

  }
}