using Microsoft.AspNetCore.Mvc;
using staticfile_sv.Interface;
using staticfile_sv.Model;

namespace staticfile_sv.Service
{
  public partial class StaticfileServiceV1 : ControllerBase , IStaticfileService
  {
    public ActionResult CreateStaticFileV1 (StaticFileModel model)
    {
      
        return Ok(model);
    }

    public ActionResult<IEnumerable<GetStaticFileModel>> GetBlog()
    {
      throw new NotImplementedException();
    }
  }
}