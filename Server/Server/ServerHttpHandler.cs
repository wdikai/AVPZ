using System;
using System.Web;

namespace Server
{
  public abstract class ServerHttpHandler : IHttpAsyncHandler
  {
    public virtual bool IsReusable { get { return false; } }

    public void ProcessRequest(HttpContext cx)
    {
      throw new NotSupportedException();
    }

    public IAsyncResult BeginProcessRequest(HttpContext cx, AsyncCallback cb, object extraData)
    {
      return ServerHttpRequest.Process(cx, cb, extraData);
    }

    public void EndProcessRequest(IAsyncResult result)
    {
      //throw new NotSupportedException();
    }
  }
}