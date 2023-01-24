using Microsoft.AspNetCore.Mvc;
using staticfile_sv.DTOs;
using staticfile_sv.Interface;
using staticfile_sv.Model;

namespace Api.StaticfileSv.Controllers;

[ApiController]
[Route("api/v1/blog")]
public class BlogController : ControllerBase
{

  private readonly ILogger<BlogController> _logger;
  private readonly IBlogService _blogService;

  public BlogController(ILogger<BlogController> logger, IBlogService blogService)
  {
    _logger = logger;
    this._blogService = blogService;
  }


  [HttpPost]

  public ActionResult<Blog> CreateBlog(CreateBlog model)
  {
    return _blogService.CreateBlog(model);
  }

  [HttpGet]
  public ActionResult<IEnumerable<Blog>> GetBlogs()

  {
    return _blogService.GetBlogs();
  }


}
