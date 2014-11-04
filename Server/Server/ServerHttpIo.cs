using System.IO;
using System.IO.Compression;
using System.Text;

namespace Server
{
  public class ServerHttpIo
  {
    public static void ReadRequestData(ServerHttpRequest request)
    {
      var httpRequest = request.HttpContext.Request;

      // Data
      if (httpRequest.HttpMethod == "GET")
      {
        var data = httpRequest.QueryString["data"];
        request.Input = data;
      }
      else
      {
        //TODO: POST
        
      }
    }

    public static void WriteResponseData(ServerHttpRequest request)
    {
      var httpResponse = request.HttpContext.Response;
      httpResponse.ContentType = "text/html; charset=utf-8";

      //WRITE
      var textOutput = request.Output;
      var buffer = Encoding.Unicode.GetBytes(textOutput);
      var responseBytes = Encoding.Convert(Encoding.Unicode, Encoding.UTF8, buffer);
      var responseChars = new char[Encoding.UTF8.GetCharCount(responseBytes, 0, responseBytes.Length)];
      Encoding.UTF8.GetChars(responseBytes, 0, responseBytes.Length, responseChars, 0);
      httpResponse.ContentEncoding = new UTF8Encoding(false);
      httpResponse.Output.Write(responseChars, 0, responseChars.Length);
    }
  }
}