using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ImageProc.Controllers
{
    public class ImageController : ApiController
    {
        public IHttpActionResult GetImage(int w, int h)
        {
            using (var img = new ImageProcessor.ImageFactory())
            {
                string virtualPath = ConfigurationManager.AppSettings["imageDoc"] + "/timg.jpg";
                string thumbnailPath = ConfigurationManager.AppSettings["imageThumbsDoc"] + "/timg.jpg";
                img.Load(FileUtil.GetMapPath(virtualPath));
                if (w > 0)
                {
                    var size = new System.Drawing.Size() { Width = w, Height = h };
                    img.Resize(new ImageProcessor.Imaging.ResizeLayer(size,
                        resizeMode: ImageProcessor.Imaging.ResizeMode.Max,
                        anchorPosition: ImageProcessor.Imaging.AnchorPosition.Center));
                }
                img.Alpha(50);
                img.Quality(50);
                img.Save(string.Format(FileUtil.GetMapPath(thumbnailPath)));
                string host = HttpContext.Current.Request.Url.Host;
                string port = HttpContext.Current.Request.Url.Port.ToString();
                string redirectPath = "http://" + host + ":" + port + thumbnailPath.Replace("~", "");
                return Redirect(redirectPath);

            }
        }
    }
}