using Microsoft.AspNetCore.Mvc;
using staticfile_sv.Interface;
using staticfile_sv.Model;

namespace Api.StaticfileSv.Controllers;

[ApiController]
[Route("api/v1/staticfile")]
public class StaticfileController : ControllerBase
{

  private readonly ILogger<StaticfileController> _logger;
  private readonly IStaticfileService _staticfileService;

  public StaticfileController(ILogger<StaticfileController> logger, IStaticfileService staticfileService)
  {
    _logger = logger;
    this._staticfileService = staticfileService;
  }


  [HttpGet]
  public ActionResult CreateStaticFileV1 (StaticFileModel model)
  {
    return _staticfileService.CreateStaticFileV1(model);
  }
}
