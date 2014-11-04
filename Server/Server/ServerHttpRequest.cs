using System;
using System.IO;
using System.Threading;
using System.Web;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Server
{
  public class ServerHttpRequest : IAsyncResult
  {
    private static readonly ILog Log = LogManager.GetLogger("AllErrors");

    public HttpContext HttpContext;

    public ServerMethodHandler MethodHandler;

    public string MethodName;
    public string UserId;

    public string Input;
    public string Output;

    private int _completed;
    private int _async;

    private AsyncCallback _cb;
    private object _extraData;

    public bool IsCompleted{ get { return _completed != 0; }}
    public bool CompletedSynchronously{ get { return _completed != 0 && _async == 0; }}
    public WaitHandle AsyncWaitHandle{ get { throw new NotSupportedException(); }}
    public object AsyncState{ get { return _extraData; } }

    private ServerHttpRequest()
    {
    }

    public static ServerHttpRequest Process(HttpContext httpContext, AsyncCallback cb, object extraData)
    {
      var r = new ServerHttpRequest
      {
        HttpContext = httpContext,
        _cb = cb,
        _extraData = extraData,
      };
      r.Process();
      return r;
    }

    private void Process()
    {
      try
      {
        ReadHeaders();
        ReadData();
        InvokeMethod();

        if (HttpContext.Response.IsClientConnected)
          WriteData();
      }

      catch (Exception e)
      {
        HttpContext.Response.Output.WriteLine(e.Message);
        Log.Error(e.Message);
      }
      Complete();
    }

    private void ReadData()
    {
      ServerHttpIo.ReadRequestData(this);
    }

    private void ReadHeaders()
    {
      var httpRequest = HttpContext.Request;

      // Check HTTP Method
      if (httpRequest.HttpMethod != "GET" && httpRequest.HttpMethod != "POST")
        throw new Exception("Unsupported HTTP method (HTTP 405 - Method Not Allowed)");

      // Read Headers
      var httpQueryString = httpRequest.QueryString;
      MethodName = httpQueryString["methodName"];
      UserId = httpQueryString["uid"] ;
      
      MethodHandler = ServerMethodManager.Instance.Value.ResolveHandler(MethodName);
    }

    private void Complete()
    {
      _completed = 1;

      // Callback
      if (_cb != null)
        _cb(this);
    }

    private void InvokeMethod()
    {
      Output = MethodHandler.Process(JObject.Parse(Input));
    }

    private void WriteData()
    {
      ServerHttpIo.WriteResponseData(this);
    }
  }
}
