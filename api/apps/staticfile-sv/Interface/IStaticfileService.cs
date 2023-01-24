using Microsoft.AspNetCore.Mvc;
using staticfile_sv.Model;

namespace staticfile_sv.Interface
{
    public interface IStaticfileService
    {
         ActionResult CreateStaticFileV1 (StaticFileModel model);
        ActionResult<IEnumerable<GetStaticFileModel>> GetBlog ();
    }
}
