using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    /// <summary>
    /// UpLoadHandler 的摘要说明
    /// </summary>
    public class UpLoadHandler : IHttpHandler
    {
        //文件上传目录
        private string uploadFolder = "UpLoad";

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            HttpFileCollection files = context.Request.Files;
            if (files.Count > 0)
            {
                string path = context.Server.MapPath(uploadFolder);
                HttpPostedFile file = files[0];

                if (file != null && file.ContentLength > 0)
                {
                    string savePath = path + "/" + context.Request.Form["fileName"];
                    file.SaveAs(savePath);
                }
            }
            else
            {
                context.Response.Write("参数错误");
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}