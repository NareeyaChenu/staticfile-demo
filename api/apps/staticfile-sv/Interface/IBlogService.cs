using Microsoft.AspNetCore.Mvc;
using staticfile_sv.DTOs;
using staticfile_sv.Model;

namespace staticfile_sv.Interface
{
    public interface IBlogService
    {
       ActionResult<Blog> CreateBlog (CreateBlog model);
       ActionResult<IEnumerable<Blog>> GetBlogs ();
    }
}
